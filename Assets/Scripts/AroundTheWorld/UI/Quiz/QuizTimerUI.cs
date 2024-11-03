using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.Quiz
{
    public class QuizTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image fillImage;
        [SerializeField] private float urgentThreshold = 3f;
        
        private float currentTime, maxTime;
        
        public void Set(float time)
        {
            currentTime = maxTime = time;
        }

        private void Update()
        {
            if (currentTime <= 0f) return;
            currentTime -= Time.deltaTime;
            
            bool isUrgent = currentTime < urgentThreshold;

            timerText.text = currentTime.ToString(isUrgent ? "F1" : "F0");
            timerText.color = isUrgent ? Color.red : Color.white;

            fillImage.fillAmount = currentTime / maxTime;
        }
    }
}