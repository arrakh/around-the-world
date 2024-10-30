using System;
using TMPro;
using UnityEngine;

namespace AroundTheWorld.Globe
{
    public class GlobeController : MonoBehaviour
    {
        [SerializeField] private ReverseGeocoder geocoder;
        [SerializeField] private TextMeshProUGUI debugText;

        private IGlobeInput[] inputs;

        public event Action<string> onLocationUpdated; 

        private void Awake()
        {
            inputs = GetComponents<IGlobeInput>();
        }

        private void Update()
        {
            if (!TryGetActiveInput(out var input))
            {
                debugText.text = "No Input Detected!";
                return;
            }
            
            input.GetInput(out var latitude, out var longitude);
            try
            {
                var country = geocoder.ReverseGeocodeCountry(longitude, latitude);
                debugText.text = country;
                onLocationUpdated?.Invoke(country);
            }
            catch (Exception e)
            {
                debugText.text = e.Message;
            }
        }

        public bool TryGetActiveInput(out IGlobeInput activeInput)
        {
            activeInput = null;
            
            foreach (var input in inputs)
            {
                if (!input.IsEnabled) continue;
                activeInput = input;
                return true;
            }

            return false;
        }
    }
}