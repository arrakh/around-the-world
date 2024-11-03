using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace AroundTheWorld.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lineText;
        [SerializeField] private float letterPause;

        public void ClearText() => lineText.text = string.Empty;
        
        public IEnumerator TypeSentence(string targetText, float textDelay)
        {
            lineText.text = "";

            // This regex pattern will match any TMP tags.
            string pattern = @"<.*?>";

            string[] parts = Regex.Split(targetText, pattern);

            int currentIndex = 0;
            foreach (string part in parts)
            {
                if (Regex.IsMatch(part, pattern))
                {
                    // If the part is a TMP tag, append it whole without delay.
                    lineText.text += part;
                }
                else
                {
                    // If the part is normal text, reveal it letter by letter.
                    foreach (char letter in part)
                    {
                        lineText.text += letter;
                        currentIndex++;
                        
                        yield return new WaitForSeconds(letterPause);
                    }
                }
            }

            yield return new WaitForSeconds(textDelay);
        }
    }
}