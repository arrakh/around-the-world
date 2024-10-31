using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AroundTheWorld.UI.Quiz
{
    public class QuizJoaoUI : MonoBehaviour
    {
        [SerializeField] private JoaoHead head;
        [SerializeField] private Image dialogueImage;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Sprite winSprite, loseSprite;

        public void Display(bool win)
        {
            resultText.text = win ? "Nice one!" : "Noooo";
            dialogueImage.gameObject.SetActive(true);
            dialogueImage.sprite = win ? winSprite : loseSprite;
            head.SetState(win ? JoaoState.HAPPY : JoaoState.SAD);
        }

        public void SetNeutral()
        {
            dialogueImage.gameObject.SetActive(false);
            head.SetState(JoaoState.DEFAULT);
        }
    }
}