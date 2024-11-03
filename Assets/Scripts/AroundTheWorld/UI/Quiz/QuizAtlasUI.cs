using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AroundTheWorld.UI.Quiz
{
    public class QuizAtlasUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private QuizAtlasLocation[] locations;

        private Dictionary<string, QuizAtlasLocation> locationsDict = new();

        private void Awake()
        {
            foreach (var location in locations)
            {
                if (locationsDict.ContainsKey(location.Location))
                {
                    Debug.LogError($"Duplicate Location {location.Location}");
                    continue;
                }

                locationsDict[location.Location] = location;
            }
        }

        public bool TryGetRandomPosition(string location, out Vector3 position)
        {
            //debug
            /*position = locations[Random.Range(0, locations.Length)].GetRandomPosition();
            return true;*/

            position = default;
            if (!locationsDict.TryGetValue(location, out var loc)) return false;
            position = loc.GetRandomPosition();
            return true;
        }
    }
}