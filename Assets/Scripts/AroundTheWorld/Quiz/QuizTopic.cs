using Arr.ScriptableDatabases;
using UnityEngine;

namespace AroundTheWorld.Quiz
{
    public class QuizTopicDatabase : ObjectScriptableDatabase<QuizTopic> {}

    [CreateAssetMenu(menuName = "Quiz/Topic")]
    public class QuizTopic : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private QuizLevel[] levels;
    }
}