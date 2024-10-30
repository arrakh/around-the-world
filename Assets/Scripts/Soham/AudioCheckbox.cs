using UnityEngine;
using UnityEngine.UI;

public class AudioCheckbox : MonoBehaviour
{
    public Sprite sprite1; // First sprite
    public Sprite sprite2; // Second sprite

    private Image buttonImage;  // Reference to the Image component of the button
    private bool isSprite1Active = true; // Tracks which sprite is currently active

    private void Start()
    {
        // Get the Image component attached to this Button GameObject
        buttonImage = GetComponent<Image>();

        // Set initial sprite
        if (buttonImage != null && sprite1 != null)
        {
            buttonImage.sprite = sprite1;
        }

        // Add a listener to the Button's onClick event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleSprite);
        }
    }

    private void ToggleSprite()
    {
        if (buttonImage == null) return;

        // Toggle the sprite between sprite1 and sprite2
        buttonImage.sprite = isSprite1Active ? sprite2 : sprite1;

        // Toggle the boolean to keep track of which sprite is active
        isSprite1Active = !isSprite1Active;

        // Additional functionality can be added here
    }
}
