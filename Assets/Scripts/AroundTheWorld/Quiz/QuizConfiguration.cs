using UnityEngine;

namespace AroundTheWorld.Quiz
{
    public static class QuizConfiguration
    {
        private static QuizTopic _currentTopic;
        
        public static QuizTopic CurrentTopic => _currentTopic;

        public static void Set(QuizTopic topic)
        {
            _currentTopic = topic;
        }
    }
}