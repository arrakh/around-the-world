using UnityEngine;

namespace AroundTheWorld.Quiz
{
    [CreateAssetMenu(menuName = "Quiz/Location")]
    public class QuizLocation : ScriptableObject
    {
        [SerializeField] private string locationId;
        [SerializeField] private string displayName;
        [SerializeField] private float longitude, latitude;
    }
}