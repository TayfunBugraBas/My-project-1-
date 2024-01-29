using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashBangEffect : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.5f;
    public Color flashColor = new Color(1f, 1f, 1f, 1f); // White color with full opacity

    public void StartFlash()
    {
        // Trigger the flash effect
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        // Display the flash
        flashImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);

        // Fade out the flash
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            flashImage.color = Color.Lerp(flashColor, Color.clear, elapsedTime / flashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the flash image is completely transparent
        flashImage.color = Color.clear;
    }
}