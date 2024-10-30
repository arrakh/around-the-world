using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteCycler : MonoBehaviour
{
    public Sprite[] spritesToCycle; // Array of sprites to cycle through
    public float cycleInterval = 0.5f; // Interval time in seconds

    private Image imageComponent;
    private int currentSpriteIndex = 0;

    private void Start()
    {
        // Get the Image component attached to this GameObject
        imageComponent = GetComponent<Image>();

        // Start the cycling coroutine
        if (spritesToCycle.Length > 0)
            StartCoroutine(CycleSprites());
    }

    private IEnumerator CycleSprites()
    {
        while (true)
        {
            // Change the sprite of the Image component to the current sprite in the array
            imageComponent.sprite = spritesToCycle[currentSpriteIndex];

            // Update the index to the next sprite, looping back to 0 if at the end of the array
            currentSpriteIndex = (currentSpriteIndex + 1) % spritesToCycle.Length;

            // Wait for the specified interval before changing the sprite again
            yield return new WaitForSeconds(cycleInterval);
        }
    }
}
