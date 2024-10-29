using System;
using UnityEngine;

namespace AroundTheWorld.Globe
{
    public class GlobeVisual : MonoBehaviour
    {
        [SerializeField] private GlobeController controller;

        private void Update()
        {
            if (!controller.TryGetActiveInput(out var input)) return;
            input.GetInput(out var lat, out var lon);
            var latLong = LatLong.FromDegrees(lat, lon);
            //transform.LookAt(latLong.Sp);

            transform.rotation = latLong.ToQuaternion();
        }
    }
}