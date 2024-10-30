using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AroundTheWorld.Globe;
using AroundTheWorld.UI.Quiz;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AroundTheWorld.Quiz
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField] private QuizPromptUI quizPromptUi;
        [FormerlySerializedAs("quizTimer")] [SerializeField] private QuizTimerUI quizTimerUI;
        [SerializeField] private GlobeController globeController;
        
        private QuizTopic topic;
        
        private int score;
        private int levelIndex;

        private bool hasLost = false;

        private string currentAnswer;

        private Queue<QuizEntry> quizQueue = new();

        private IEnumerator Start()
        {
            globeController.onLocationUpdated += OnLocationUpdated;
            topic = QuizConfiguration.CurrentTopic;
            if (topic == null) throw new Exception("TOPIC IS NOT SET IN QUIZ CONFIGURATION");

            levelIndex = 0;

            //Game Loop
            while (!hasLost && levelIndex < topic.Levels.Length)
            {
                var level = InitializeLevel();

                while (!hasLost && quizQueue.Count > 0)
                {
                    var timer = level.TimerPerQuestion;
                    var entry = quizQueue.Dequeue();
                    
                    InitializeEntry(timer, entry);

                    yield return new WaitForSeconds(timer);

                    EvaluateAnswer(entry);
                }

                levelIndex++;
            }

            if (hasLost)
            {
                Debug.Log("LOST!!!");
            }
            else
            {
                Debug.Log("WIN!!!");
            }
        }

        private void EvaluateAnswer(QuizEntry entry)
        {
            var answer = entry.AnswerLocation.Trim().ToLowerInvariant();
            if (!answer.Equals(currentAnswer, StringComparison.InvariantCultureIgnoreCase))
                hasLost = true;
        }

        private void InitializeEntry(float timer, QuizEntry entry)
        {
            quizTimerUI.Set(timer);

            quizPromptUi.DisplayPrompt(entry.QuestionText, entry.QuestionImage);
            quizPromptUi.ClearAnswer();
        }

        private QuizLevel InitializeLevel()
        {
            var level = topic.Levels[levelIndex];

            var randomized = level.Entries.OrderBy(x => Random.Range(0, level.Entries.Length));
            foreach (var entry in randomized)
                quizQueue.Enqueue(entry);
            return level;
        }


        private void OnLocationUpdated(string loc)
        {
            currentAnswer = loc.Trim().ToLowerInvariant();
            quizPromptUi.DisplayAnswer(loc);
        }
    }
}
