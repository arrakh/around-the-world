using System;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.Quiz
{
    [Serializable]
    public class QuizEntry
    {
        [SerializeField] private string questionText;
        [SerializeField] private Sprite questionImage;
        [SerializeField] private string answerLocationId;
    }
}