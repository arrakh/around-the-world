using System;
using UnityEngine;

namespace AroundTheWorld.Globe
{
    public class MouseGlobeInput : MonoBehaviour, IGlobeInput
    {
        [SerializeField] private float currentLatitude, currentLongitude;
        [SerializeField] private Vector2 latitudeRange = new(-90f, 90f);
        [SerializeField] private Vector2 longitudeRange = new(-180f, 180f);
        [SerializeField] private float mouseSpeed = 2f;

        private Vector3 lastMousePos;

        public bool IsEnabled => enabled;

        private void Awake()
        {
            lastMousePos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            var speed = mouseSpeed * Time.deltaTime;
            currentLatitude = Mathf.Clamp(currentLatitude + deltaY * speed, latitudeRange.x, latitudeRange.y);
            currentLongitude = Mathf.Clamp(currentLongitude + deltaX * speed, longitudeRange.x, longitudeRange.y);

            lastMousePos = Input.mousePosition;
        }
        
        public void GetInput(out float latitude, out float longitude)
        {
            latitude = currentLatitude;
            longitude = currentLongitude;
        }
    }
}