using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform textRectTransform;  // The RectTransform of the button text
    public float hoverOffset = 10f;          // Amount to move up on hover
    public float animationSpeed = 5f;        // Speed of the smooth animation

    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private bool isHovered = false;

    void Start()
    {
        // Store the original position of the text
        if (textRectTransform == null)
            textRectTransform = GetComponentInChildren<Text>().rectTransform;

        originalPosition = textRectTransform.anchoredPosition;
        targetPosition = originalPosition;
    }

    void Update()
    {
        // Smoothly move text to the target position
        textRectTransform.anchoredPosition = Vector2.Lerp(
            textRectTransform.anchoredPosition,
            targetPosition,
            Time.deltaTime * animationSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the target position to move the text upwards
        targetPosition = originalPosition + new Vector2(0, hoverOffset);
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the target position to the original
        targetPosition = originalPosition;
        isHovered = false;
    }
}
