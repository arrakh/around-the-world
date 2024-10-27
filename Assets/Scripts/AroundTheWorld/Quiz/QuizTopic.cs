using UnityEngine;

namespace AroundTheWorld.Quiz
{
    [CreateAssetMenu(menuName = "Quiz/Topic")]
    public class QuizTopic : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private QuizLevel[] levels;
    }
}