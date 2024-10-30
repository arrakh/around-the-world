using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

namespace AroundTheWorld.UI
{
    public enum JoaoState
    {
        DEFAULT,
        HAPPY,
        SAD
    }
    
    public class JoaoHead : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform rotateRoot;
        [SerializeField] private Vector2 rotateRange = new (-8, 8);
        [SerializeField] private float rotateDelay = 0.8f;

        [Header("Joao State Sprites")]
        [SerializeField] private Image joaoImage;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite happySprite;
        [SerializeField] private Sprite sadSprite; 

        private float currentDelay;
        
        private void Update()
        {
            currentDelay -= Time.deltaTime;

            if (currentDelay > 0f) return;
            currentDelay = rotateDelay;
            rotateRoot.rotation = Quaternion.Euler(0f, 0f, Random.Range(rotateRange.x, rotateRange.y));
        }

        public void SetState(JoaoState state)
        {
            switch (state)
            {
                case JoaoState.DEFAULT:
                    joaoImage.sprite = defaultSprite;
                    animator.SetTrigger("Default");
                    break;
                case JoaoState.HAPPY:
                    joaoImage.sprite = happySprite;
                    animator.SetTrigger("Happy");
                    break;
                case JoaoState.SAD:
                    joaoImage.sprite = sadSprite;
                    animator.SetTrigger("Sad");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}