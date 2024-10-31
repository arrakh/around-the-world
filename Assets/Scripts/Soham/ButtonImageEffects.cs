using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class ButtonImageEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image buttonImage;         // Reference to the Image component
    public float squiggleAmplitude = 5f; // Amplitude of the squiggle effect
    public float squiggleFrequency = 5f; // Frequency of the squiggle effect
    public float bounceHeight = 20f;     // Height of the bounce on click
    public float bounceDuration = 0.2f;  // Duration of the bounce animation

    private bool isHovered = false;
    private Vector3 originalPosition;
    private float squiggleTime = 0f;

    void Start()
    {
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();

        originalPosition = buttonImage.rectTransform.localPosition;
    }

    void Update()
    {
        // Apply squiggle effect when hovered
        if (isHovered)
        {
            squiggleTime += Time.deltaTime * squiggleFrequency;
            float offset = Mathf.Sin(squiggleTime) * squiggleAmplitude;
            buttonImage.rectTransform.localPosition = originalPosition + new Vector3(offset, 0, 0);
        }
        else
        {
            // Reset to original position when not hovered
            buttonImage.rectTransform.localPosition = Vector3.Lerp(
                buttonImage.rectTransform.localPosition,
                originalPosition,
                Time.deltaTime * 10f
            );
            squiggleTime = 0f;  // Reset squiggle time
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(BounceAnimation());
    }

    private IEnumerator BounceAnimation()
    {
        // Move up
        float elapsedTime = 0f;
        Vector3 startPosition = originalPosition;
        Vector3 targetPosition = originalPosition + new Vector3(0, bounceHeight, 0);

        while (elapsedTime < bounceDuration / 2)
        {
            buttonImage.rectTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (bounceDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset time and move back down
        elapsedTime = 0f;
        startPosition = buttonImage.rectTransform.localPosition;

        while (elapsedTime < bounceDuration / 2)
        {
            buttonImage.rectTransform.localPosition = Vector3.Lerp(startPosition, originalPosition, elapsedTime / (bounceDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends at the original position
        buttonImage.rectTransform.localPosition = originalPosition;
    }
}
