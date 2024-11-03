using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AroundTheWorld.UI.Quiz
{
    public class QuizAtlasLocation : MonoBehaviour
    {
        [SerializeField] private string location;
        [SerializeField] private float randomRange;

        public string Location => location;
        public float Range => randomRange;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(location)) location = name;
        }

        public Vector3 GetRandomPosition()
            => transform.position + (Vector3) (Random.insideUnitCircle * randomRange);
    }
}