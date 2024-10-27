using UnityEngine;

namespace AroundTheWorld.Quiz
{
    [CreateAssetMenu(menuName = "Quiz/Level")]
    public class QuizLevel : ScriptableObject
    {
        [SerializeField] private float timerPerQuestion = 5f;
        [SerializeField] private QuizEntry[] entries;
    }
}