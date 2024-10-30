using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AroundTheWorld.Quiz
{
    public class QuizTopicLoader : MonoBehaviour
    {
        [SerializeField] private QuizTopic testTopic;
        public void LoadQuiz()
        { // The method that loads the next scene
            QuizConfiguration.Set(testTopic);
            SceneManager.LoadScene("Quiz");
        }
    }
}