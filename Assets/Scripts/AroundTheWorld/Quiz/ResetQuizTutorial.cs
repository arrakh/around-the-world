using UnityEngine;

namespace AroundTheWorld.Quiz
{
    public class ResetQuizTutorial : MonoBehaviour
    {
        public void ResetTutorial()
        {
            PlayerPrefs.DeleteKey(QuizController.HAS_SEEN_TUTORIAL);
        }
    }
}