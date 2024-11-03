using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.UI
{
    public class FadeUI : MonoBehaviour
    {
        [SerializeField] private Image blackImage;

        private Tween tween;
        
        public void FadeIn(float duration)
        {
            Init();
            blackImage.color = new Color(0f, 0f, 0f, 0f);
            tween = blackImage.DOFade(1f, duration).OnComplete(OnDoneFade);
        }

        public void FadeOut(float duration)
        {
            Init();
            blackImage.color = new Color(0f, 0f, 0f, 1f);
            tween = blackImage.DOFade(0f, duration).OnComplete(OnDoneFade);
        }

        private void Init()
        {
            blackImage.gameObject.SetActive(true);
            tween?.Kill();
        }

        private void OnDoneFade()
        {
            blackImage.gameObject.SetActive(false);
        }
    }
}