using UnityEngine;

namespace AroundTheWorld.Globe
{
    public class ArduinoGlobeInput : MonoBehaviour, IGlobeInput
    {
        [SerializeField] private float currentLatitude = 0f;
        [SerializeField] private float currentLongitude = 0f;
        
        // Range for potentiometer values (latitude)
        private const int maxPotentiometerValue = 1023;
        private const int minPotentiometerValue = 92; // I cucked it because at 90 degrees it's 1023 and it's 92 at the point where it gets stuck at the bottom, there's an error but it will be okay if you hot glue the rotor

        // Encoder values for determining longitude
        private int previousEncoderValue = 0;
        private float cumulativeRotation = 0f;
        private const float encoderToDegrees = 9f; // Each encoder unit corresponds to 9 degrees (40 encoder units == 360 degrees) - I did this because again, the values go from 0 to 40 for a full 360 degree rotation west-east and 0 to -40 for east-west
        
        private SerialController serialController;
        
        public bool IsEnabled => enabled;

        private bool shouldResetLongitude = true;
        
        private void Awake()
        {
            serialController = GetComponent<SerialController>();
            if (serialController == null)
            {
                Debug.LogError("SerialController component is missing on this GameObject.");
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space)) shouldResetLongitude = true;

            if (serialController != null)
            {
                string message = serialController.ReadSerialMessage();
                if (message != null)
                {
                    ProcessArduinoInput(message);
                    // Log the current latitude and longitude values to the console
                }
            }
        }
        private void ProcessArduinoInput(string message)
        {
            string[] values = message.Split(',');
            if (values.Length == 2 && int.TryParse(values[0], out int encoderValue) && int.TryParse(values[1], out int potentiometerValue))
            {
                // Calculate latitude and longitude
                currentLatitude = CalculateLatitude(potentiometerValue);
                currentLongitude = CalculateLongitude(encoderValue);

                // Print the results
                //Debug.Log($"Latitude: {currentLatitude}, Longitude: {currentLongitude}, Potentiometer: {potentiometerValue}, Encoder: {encoderValue}");
            }
        }

        private float CalculateLatitude(int potentiometerValue)
        {
            // Map potentiometer range (1023 to 92) to latitude range (-90 to 90)
            float latitude = Mathf.Lerp(-41.2f, 90f, (float)(potentiometerValue - minPotentiometerValue) / (maxPotentiometerValue - minPotentiometerValue));
            return latitude;
        }

        private float CalculateLongitude(int encoderValue)
        {
            if (shouldResetLongitude)
            {
                previousEncoderValue = encoderValue;
                cumulativeRotation = 0f;
                shouldResetLongitude = false;
                return 0;
            }
            
            // Detect rotation direction and calculate cumulative rotation
            int delta = encoderValue - previousEncoderValue;

            // Adjust for wrap-around (assuming the error is Â±1) You might wanna tinker with the error margin
            if (delta > 500) delta -= 1023;
            else if (delta < -500) delta += 1023;

            // Increment cumulative rotation by the degree-equivalent of the delta change
            cumulativeRotation += delta * encoderToDegrees;
            previousEncoderValue = encoderValue;
            
            //Debug.Log($"cumulative is now {cumulativeRotation}");

            // Map cumulative rotation to longitude range (-180 to 180)
            float longitude = cumulativeRotation % 360f;  // Wrap within 0 to 360
            if (longitude > 180f) longitude -= 360f;       // Convert to -180 to 180 range
            else if (longitude < -180f) longitude += 360f;

            return longitude;
        }

        public void GetInput(out float latitude, out float longitude)
        {
            latitude = currentLatitude;
            longitude = currentLongitude;
        }
    }
}