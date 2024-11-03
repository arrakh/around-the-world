using AroundTheWorld.Quiz;
using AroundTheWorld.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Soham
{
    public class QuizTopicLoader : MonoBehaviour
    {
        [SerializeField] private QuizTopic testTopic;
        [SerializeField] private FadeUI fadeUi;
        
        private bool hasLoaded = false;
        
        public void LoadQuiz()
        {
            if (hasLoaded) return; //makes it so you can only click once

            hasLoaded = true;
            
            fadeUi.FadeIn(1f);
            Invoke(nameof(InternalLoad), 1f);
        }

        private void InternalLoad()
        {
            QuizConfiguration.Set(testTopic);
            SceneManager.LoadScene("Quiz");
        }
    }
}