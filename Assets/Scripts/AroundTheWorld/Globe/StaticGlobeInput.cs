using System;
using UnityEngine;

namespace AroundTheWorld.Globe
{
    public class StaticGlobeInput : MonoBehaviour, IGlobeInput
    {
        [SerializeField] private float currentLatitude, currentLongitude;
        
        public bool IsEnabled => enabled;

        public void GetInput(out float latitude, out float longitude)
        {
            latitude = currentLatitude;
            longitude = currentLongitude;
        }

        private void Update()
        {
            
        }
    }
}