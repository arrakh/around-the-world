using System;
using TMPro;
using UnityEngine;

namespace AroundTheWorld.UI
{
    public class ComPortInput : MonoBehaviour
    {
        public static string PORT = "COM10";

        [SerializeField] private TMP_InputField inputField;

        private void Awake()
        {
            inputField.onValueChanged.AddListener(input => PORT = input);
        }
    }
}