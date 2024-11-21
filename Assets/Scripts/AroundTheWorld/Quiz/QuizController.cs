using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AroundTheWorld.Globe;
using AroundTheWorld.UI;
using AroundTheWorld.UI.Quiz;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace AroundTheWorld.Quiz
{
    public class QuizController : MonoBehaviour
    {
        [SerializeField] private QuizPromptUI quizPromptUi;
        [SerializeField] private QuizTimerUI quizTimerUI;
        [SerializeField] private GlobeController globeController;
        [SerializeField] private QuizAtlasUI atlasUi;
        [SerializeField] private QuizPromptUI quizPromptPrefab;
        [SerializeField] private QuizJoaoUI joaoUi;
        [SerializeField] private JoaoScreen joaoScreen;
        [SerializeField] private FadeUI fadeUi;
        
        private QuizTopic topic;
        
        private int score;
        private int levelIndex;

        private bool hasLost = false;

        private string currentAnswer = String.Empty;

        private Queue<QuizEntry> quizQueue = new();

        public const string HAS_SEEN_TUTORIAL = "has-seen-tutorial";

        private IEnumerator Start()
        {
            quizPromptUi.gameObject.SetActive(false);

            globeController.onLocationUpdated += OnLocationUpdated;
            topic = QuizConfiguration.CurrentTopic;
            joaoUi.SetNeutral();
            
            if (topic == null) throw new Exception("TOPIC IS NOT SET IN QUIZ CONFIGURATION");

            levelIndex = 0;
            
            bool hasSeenTutorial = PlayerPrefs.HasKey(HAS_SEEN_TUTORIAL);
            if (!hasSeenTutorial) yield return TutorialSequence();
            
            joaoScreen.gameObject.SetActive(false);
            fadeUi.FadeOut(1f);
            yield return new WaitForSeconds(1f);

            //Game Loop
            while (!hasLost && levelIndex < topic.Levels.Length)
            {
                var level = InitializeLevel();

                while (quizQueue.Count > 0)
                {
                    joaoUi.SetNeutral();

                    quizPromptUi.gameObject.SetActive(true);
                    quizPromptUi.AnimateIn();
                    
                    var timer = level.TimerPerQuestion;
                    var entry = quizQueue.Dequeue();
                    
                    InitializeEntry(timer, entry);

                    yield return new WaitForSeconds(timer);

                    var answer = entry.AnswerLocation.Trim().ToLowerInvariant();
                    
                    bool isAnswerCorrect = currentAnswer.Contains(answer, StringComparison.InvariantCultureIgnoreCase);
                    
                    joaoUi.Display(isAnswerCorrect, entry.AnswerLocation);

                    if (!hasLost && !isAnswerCorrect) hasLost = true;

                    if (isAnswerCorrect) yield return OnAnswerCorrect(entry.AnswerLocation);
                    else yield return new WaitForSeconds(1f);
                }

                levelIndex++;
            }

            yield return new WaitForSeconds(1f);
            
            fadeUi.FadeIn(2f);
            yield return new WaitForSeconds(2f);

            joaoScreen.gameObject.SetActive(true);
            joaoScreen.ClearText();
            
            fadeUi.FadeOut(0.6f);
            yield return new WaitForSeconds(0.6f);

            yield return joaoScreen.Display($"Game is over! you collected {score} cards!", 2f, JoaoState.DEFAULT);

            if (hasLost) yield return joaoScreen.Display("Unfortunately that's not enough to proceed to the next level, try again next time!", 2f, JoaoState.SAD);
            else yield return joaoScreen.Display("You completed all the questions, Congratulations! Now to another topic!", 2f, JoaoState.HAPPY);
            
            fadeUi.FadeIn(2f);
            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene("MainMenu");
        }

        private IEnumerator TutorialSequence()
        {
            joaoScreen.gameObject.SetActive(true);
            
            fadeUi.FadeOut(1f);
            yield return new WaitForSeconds(1f);
            
            yield return joaoScreen.Display($"Welcome to Around The World!", 2f, JoaoState.HAPPY);
            yield return joaoScreen.Display($"To win, collect cards by answering where it originates from!", 2f, JoaoState.DEFAULT);
            yield return joaoScreen.Display($"To answer, spin both dials on the bottom of the globe to select a country!", 2f, JoaoState.DEFAULT);
            yield return joaoScreen.Display($"Good luck!", 2f, JoaoState.HAPPY);
            
            PlayerPrefs.SetInt(HAS_SEEN_TUTORIAL, 0);
            
            fadeUi.FadeIn(3f);
            yield return new WaitForSeconds(3f);
        }
        
        private IEnumerator OnAnswerCorrect(string entryAnswerLocation)
        {
            score++;
            if (!atlasUi.TryGetRandomPosition(entryAnswerLocation, out var position)) yield break;
            quizPromptUi.gameObject.SetActive(false);

            var fakePrompt = Instantiate(quizPromptPrefab);
            fakePrompt.Copy(quizPromptUi);
            fakePrompt.SetSortingOrder(-1);

            yield return fakePrompt.AnimateToAtlas(position);
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
            quizQueue = new Queue<QuizEntry>(level.Entries.Length);

            var randomized = level.Entries.OrderBy(x => Random.Range(int.MinValue, int.MaxValue));
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
