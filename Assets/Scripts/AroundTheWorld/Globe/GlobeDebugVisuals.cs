using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Wibci.CountryReverseGeocode;
using Wibci.CountryReverseGeocode.Data;
using Random = UnityEngine.Random;

namespace AroundTheWorld.Globe
{
    public class GlobeDebugVisuals : MonoBehaviour
    {
        [SerializeField] TextAsset countriesJson;
        [SerializeField] private int locationCount = 4;
        [SerializeField] private float radius = 1f;
        [SerializeField] private float pointSize = 0.2f;

        [SerializeField] private bool showText = true;
        [SerializeField] private bool useRange = true;
        [SerializeField] private Vector2Int indexRange;

        private Dictionary<string, Color> colors = new();

        private JsonDataProvider provider;
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (provider == null) InitializeService();

            var allData = provider.Data;

            if (useRange)
            {
                if (indexRange.x < 0) indexRange.x = 0;
                if (indexRange.x > indexRange.y) indexRange.x = indexRange.y;
                if (indexRange.y > provider.Data.Count - 1) indexRange.y = provider.Data.Count - 1;
                allData = provider.Data.GetRange(indexRange.x, indexRange.y - indexRange.x);
            }

            foreach (var data in allData)
            {
                Color color;
                if (!colors.TryGetValue(data.name, out color))
                {
                    color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                    colors[data.name] = color;
                }

                Vector3 totalPos = Vector3.zero;
                
                int count = 0;
                foreach (var bigCoord in data.coordinates)
                {
                    foreach (var coord in bigCoord)
                    {
                        var pos = GetPosition(coord[0], coord[1], radius);
                        Gizmos.color = color;
                        Gizmos.DrawCube(pos, Vector3.one * pointSize);

                        totalPos += pos;
                    }

                    count++;
                    if (count >= locationCount) break;
                }

                if (!showText) continue;
                
                GUIStyle style = new GUIStyle();
                style.normal.textColor = color;
                style.fontStyle = FontStyle.Bold;
                style.normal.background = Texture2D.blackTexture;
                
                var averagePos = totalPos / data.coordinates.Count;
                var textPos = averagePos.normalized * radius;
                Handles.Label(textPos, data.name, style);
            }
        }
        #endif

        private Vector3 GetPosition(double lon, double lat, float distance)
        {
            var latLong = LatLong.FromDegrees((float) lat, (float)lon);
            return latLong.ToDirection() * distance;
        }

        private void InitializeService()
        {
            provider = new JsonDataProvider(countriesJson.text);
        }
    }
}