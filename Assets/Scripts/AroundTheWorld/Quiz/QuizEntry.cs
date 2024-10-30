using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AroundTheWorld.Quiz
{
    [Serializable]
    public class QuizEntry
    {
        [SerializeField] private string questionText;
        [SerializeField] private Sprite questionImage;
        [SerializeField] private string answerLocation;

        public string QuestionText => questionText;

        public Sprite QuestionImage => questionImage;

        public string AnswerLocation => answerLocation;
    }
}