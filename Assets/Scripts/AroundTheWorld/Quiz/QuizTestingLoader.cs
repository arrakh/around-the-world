using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AroundTheWorld.Quiz
{
    public class QuizTestingLoader : MonoBehaviour
    {
        [SerializeField] private QuizTopic testTopic;
        
        private void Start()
        {
            QuizConfiguration.Set(testTopic);
            SceneManager.LoadScene("Quiz");
        }
    }
}