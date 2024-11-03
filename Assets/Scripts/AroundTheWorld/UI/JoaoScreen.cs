using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AroundTheWorld.UI.Quiz
{
    public class JoaoScreen : MonoBehaviour
    {
        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private JoaoHead joaoHead;
        [SerializeField] private JoaoState currentState = JoaoState.DEFAULT;

        public void ClearText() => dialogueUI.ClearText();

        public IEnumerator Display(string text, float textDelay, JoaoState newState)
        {
            UpdateJoaoState(newState);
            return dialogueUI.TypeSentence(text, textDelay);
        }

        public void UpdateJoaoState(JoaoState newState)
        {
            if (joaoHead == null)
            {
                Debug.LogError("JoaoHead reference is not assigned!");
                return;
            }

            
            joaoHead.SetState(newState);
            currentState = newState;
            Debug.Log($"Joao's state changed to: {newState}");
        }

        //functions to call to change state (trigger) 
        public void SetJoaoToHappy() => UpdateJoaoState(JoaoState.HAPPY);
        public void SetJoaoToSad() => UpdateJoaoState(JoaoState.SAD);
        public void SetJoaoToDefault() => UpdateJoaoState(JoaoState.DEFAULT);
    }

}
