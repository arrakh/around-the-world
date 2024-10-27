using Arr.ScriptableDatabases;
using UnityEngine;

namespace AroundTheWorld.Quiz
{
    public class QuizLocationDatabase : ObjectScriptableDatabase<QuizLocation> {}

    [CreateAssetMenu(menuName = "Quiz/Location")]
    public class QuizLocation : ScriptableObject
    {
        [SerializeField] private string locationId;
        [SerializeField] private string displayName;
        [SerializeField] private float longitude, latitude;
    }
}