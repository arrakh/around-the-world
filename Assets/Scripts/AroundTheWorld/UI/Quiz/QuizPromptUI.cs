using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AroundTheWorld.UI.Quiz
{
    [RequireComponent(typeof(Canvas))]
    public class QuizPromptUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private Image promptImage;
        [SerializeField] private TextMeshProUGUI answerText;

        [SerializeField] private Transform holder;

        [Header("Atlas Animation")] 
        [SerializeField] private Vector2 scaleFromTo;
        [SerializeField] private float animDuration = 0.6f;
        [SerializeField] private float afterAnimDelay = 0.4f;
        [SerializeField] private AnimationCurve scaleCurve, positionCurve;

        [Header("In Animation")] 
        [SerializeField] private Vector2 localXPosFromTo;
        [SerializeField] private Vector2 localZRotFromTo;
        [SerializeField] private float inAnimDuration = 1.2f;
        [SerializeField] private AnimationCurve inPosCurve, inRotCurve;
        
        private Canvas canvas;

        private Tween scaleTween, positionTween, inPosTween, inRotTween;
        
        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        public void SetSortingOrder(int order) => canvas.sortingOrder = order;

        public void AnimateIn()
        {
            inPosTween?.Kill();
            inRotTween?.Kill();

            var localPos = holder.localPosition;
            localPos.x = localXPosFromTo.x;
            holder.localPosition = localPos;

            inPosTween = holder.DOLocalMoveX(localXPosFromTo.y, inAnimDuration).SetEase(inPosCurve);
            
            holder.rotation = Quaternion.Euler(0f, 0f, localZRotFromTo.x);
            inRotTween = holder.DORotate(new Vector3(0f, 0f, localZRotFromTo.y), animDuration).SetEase(inRotCurve);
        }

        public IEnumerator AnimateToAtlas(Vector3 pixelPosition)
        {
            scaleTween?.Kill();
            positionTween?.Kill();

            holder.localScale = Vector3.one * scaleFromTo.x;
            scaleTween = holder.DOScale(scaleFromTo.y, animDuration).SetEase(scaleCurve);
            positionTween = holder.DOMove(pixelPosition, animDuration).SetEase(positionCurve);

            yield return new WaitForSeconds(animDuration + afterAnimDelay);
        }

        public void DisplayPrompt(string text, Sprite image)
        {
            promptText.text = text;
            promptImage.sprite = image;
        }

        public void DisplayAnswer(string text) => answerText.text = text;

        public void ClearAnswer() => answerText.text = String.Empty;

        public void Copy(QuizPromptUI other)
        {
            promptText.text = other.promptText.text;
            promptImage.sprite = other.promptImage.sprite;
            answerText.text = other.answerText.text;
        }
    }
}