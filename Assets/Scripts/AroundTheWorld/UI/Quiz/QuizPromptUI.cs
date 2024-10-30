using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.UI.Quiz
{
    public class QuizPromptUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private Image promptImage;
        [SerializeField] private TextMeshProUGUI answerText;

        public void DisplayPrompt(string text, Sprite image)
        {
            promptText.text = text;
            promptImage.sprite = image;
        }

        public void DisplayAnswer(string text) => answerText.text = text;

        public void ClearAnswer() => answerText.text = String.Empty;

        public void Copy(QuizPromptUI other)
        {
            promptText.text = other.promptText.text;
            promptImage.sprite = other.promptImage.sprite;
            answerText.text = other.answerText.text;
        }
    }
}