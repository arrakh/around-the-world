using System;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.Utilities
{
    [ExecuteAlways] [RequireComponent(typeof(RawImage))]
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] RawImage image;
        [SerializeField] Vector2 speed = Vector2.one;

        void Update()
        {
            image.uvRect = new Rect(image.uvRect.position + new Vector2(speed.x, speed.y) * Time.deltaTime, image.uvRect.size);
        }

        private void OnValidate()
        {
            if (!image) image = GetComponent<RawImage>();
        }
    }
}