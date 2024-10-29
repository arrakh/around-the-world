using UnityEngine;

namespace AroundTheWorld
{
    public struct LatLong
    {
        public const float PI = Mathf.PI;
        public const float TWO_PI = 2f * PI;
        public const float PI_HALF = .5f * PI;

        public static LatLong nPole { get; } = new(0f, 0f);

        public static LatLong sPole { get; } = new(PI, TWO_PI, true);

        public static LatLong forward { get; } = new(PI_HALF, 0f);

        public static LatLong back { get; } = new(PI_HALF, PI);

        public static LatLong left { get; } = new(PI_HALF, -PI_HALF);

        public static LatLong right { get; } = new(PI_HALF, PI_HALF);

        // Forward vector (0, 0, 1) is Lat: 90, Long: 0 (in radians)
        // Lat 0 is North pole, Lat 180 is South pole (in radians)
        public static LatLong FromDirection(Vector3 direction)
        {
            var latitude = Mathf.Acos(direction.y); // latitude (phi) normally acos(v.y/radius)
            if (float.IsNaN(latitude)) latitude = direction.y > 0f ? 0f : PI;
            var longitude = Mathf.Atan2(direction.x, direction.z); // longitude (theta)
            return new LatLong(latitude, longitude);
        }

        public static LatLong FromQuaternion(Quaternion r)
        {
            return FromDirection(r * Vector3.forward);
        }

        // Lat: 0-180; Long: 0-360 (in radians)
        public static Quaternion ToQuaternion(float latitude, float longitude)
        {
            if (float.IsNaN(latitude) || latitude <= 0f) return Quaternion.Euler(-90f, 0f, 0f);

            if (latitude >= PI)
            {
                return Quaternion.Euler(90f, 0f, 0f);
            }

            float ctheta = Mathf.Cos(longitude * .5f), cphi = Mathf.Cos(latitude * .5f);
            float stheta = Mathf.Sin(longitude * .5f), sphi = Mathf.Sin(latitude * .5f);

            // normally "cphi * ctheta, -sphi * stheta, cphi * stheta, sphi * ctheta", but X and Y have been negated in order to flip Z
            // according to https://stackoverflow.com/questions/32438252/efficient-way-to-apply-mirror-effect-on-quaternion-rotation

            return new Quaternion(-cphi * ctheta, sphi * stheta, cphi * stheta, sphi * ctheta) *
                   Quaternion.Euler(90f, 0f, 0f);
        }

        public static Quaternion ToQuaternion(LatLong latLong)
        {
            return ToQuaternion(latLong.latitude, latLong.longitude);
        }

        public static Quaternion ToQuaternion(Vector3 direction)
        {
            return FromDirection(direction).ToQuaternion();
        }

        private float _lat;

        public float latitude
        {
            get => _lat;
            set => _lat = _unwind(value, PI);
        }

        private float _lon;

        public float longitude
        {
            get => _lon;
            set => _lon = _unwind(value, TWO_PI);
        }

        private float _unwind(float v, float m)
        {
            v = v % m;
            if (v < 0f) v += m;
            return v;
        }

        // arbitrary is used to bypass unwinding of the angles
        // it's just a workaround for some specific cases, not useful in a general sense
        public LatLong(float latitude, float longitude, bool arbitrary = false)
        {
            _lat = latitude;
            _lon = longitude;
            if (!arbitrary) Set(latitude, longitude);
        }

        public void Set(float latitude, float longitude)
        {
            _lat = _unwind(latitude, PI);
            _lon = _unwind(longitude, TWO_PI);
        }

        // skips unwinding of the angles
        public void SetArbitrary(float latitude, float longitude)
        {
            _lat = latitude;
            _lon = longitude;
        }

        // takes care of the polar transition
        public LatLong Rotate(float latitudeDelta, float longitudeDelta)
        {
            longitude += longitudeDelta;
            latitude += latitudeDelta + (_lat < 0f || _lat > PI ? PI : 0f);
            if (_lat < 0f) _lat = -_lat;
            else if (_lat > PI) _lat -= PI;
            return this;
        }

        public LatLong Rotate(Vector2 delta)
        {
            return Rotate(delta.x, delta.y);
        }

        public LatLong Inverse => new(PI - _lat, PI + _lon);

        /// <summary>
        /// This method assumes latitude longitude in degrees with
        /// Latitude ranging from -90 to 90
        /// Longitude ranging from -180 to 180
        /// </summary>
        /// <param name="latitudeDeg"></param>
        /// <param name="longitudeDeg"></param>
        /// <returns></returns>
        public static LatLong FromDegrees(float latitudeDeg, float longitudeDeg)
        {
            // Map latitude from -90° to 90° to the struct's 0 to π
            float latitudeRad = (latitudeDeg + 90f) * (Mathf.PI / 180f);

            // Map longitude from -180° to 180° to the struct's 0 to 2π
            float longitudeRad = (longitudeDeg + 180f) * (Mathf.PI / 180f);

            return new LatLong(latitudeRad, longitudeRad);
        }

        // this is garbage, as it was never used with hashmaps
        // I recommend using Cantor pairing with the two hash codes instead
        public override int GetHashCode()
        {
            return _lon.GetHashCode() ^ (_lat.GetHashCode() << 2) ^ 1157;
        }

        public bool Equals(LatLong other)
        {
            return _lat.Equals(other._lat) && _lon.Equals(other._lon);
        }

        public override bool Equals(object obj)
        {
            if (obj is LatLong ll) return Equals(ll);
            return false;
        }

        public static bool operator ==(LatLong a, LatLong b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LatLong a, LatLong b)
        {
            return !a.Equals(b);
        }

        public static LatLong operator -(LatLong a, LatLong b)
        {
            return new LatLong(a.latitude - b.latitude, a.longitude - b.longitude);
        }

        public static LatLong operator +(LatLong a, LatLong b)
        {
            return new LatLong(a.latitude + b.latitude, a.longitude + b.longitude);
        }

        public static LatLong operator +(LatLong a, Vector2 b)
        {
            return new LatLong(a.latitude + b.x, a.longitude + b.y);
        }

        public static LatLong operator +(Vector2 a, LatLong b)
        {
            return b + a;
        }

        public static LatLong operator *(float n, LatLong ll)
        {
            return new LatLong(ll.latitude * n, ll.longitude * n);
        }

        public static LatLong operator *(LatLong ll, float n)
        {
            return n * ll;
        }

        public static LatLong operator /(LatLong ll, float n)
        {
            return new LatLong(ll.latitude / n, ll.longitude / n);
        }

        public static implicit operator Vector2(LatLong ll)
        {
            return new Vector2(ll._lat, ll._lon);
        }

        public static implicit operator LatLong(Vector2 v)
        {
            return new LatLong(v.x, v.y);
        }

        public static explicit operator Quaternion(LatLong ll)
        {
            return ll.ToQuaternion();
        }

        public static explicit operator LatLong(Quaternion q)
        {
            return FromQuaternion(q);
        }

        public override string ToString()
        {
            var degs = ToDegrees();
            return string.Format("[LatLong: lat={0:0.000}°, lon={1:0.000}°]", degs.x, degs.y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(_lat, _lon);
        }

        public Vector2 ToDegrees()
        {
            return ToVector2() * Mathf.Rad2Deg;
        }

        public Vector3 ToDirection()
        {
            return ToQuaternion() * Vector3.forward;
        }

        public Quaternion ToQuaternion()
        {
            return ToQuaternion(this);
        }
    }
}