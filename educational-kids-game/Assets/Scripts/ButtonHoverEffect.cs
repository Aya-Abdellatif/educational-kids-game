using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale; // To store the original scale of the button
    [HideInInspector]
    public bool isHovered; // To track if the pointer is over the button

    void Start()
    {
        // Store the initial scale of the button
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true; // Mark as hovered
        StopAllCoroutines(); // Stop any running animations
        StartCoroutine(ScaleToTarget(originalScale * 1.3f)); // Scale up
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false; // Mark as not hovered
        StopAllCoroutines(); // Stop any running animations
        StartCoroutine(ScaleToTarget(originalScale)); // Scale back to original size
    }

    private IEnumerator ScaleToTarget(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale; // Current scale
        float duration = 0.25f; // Animation duration
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Smoothly interpolate between the current scale and the target scale
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the scale is exactly the target scale at the end
        transform.localScale = targetScale;
    }
}
