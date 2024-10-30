using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
            
        }
    }
}