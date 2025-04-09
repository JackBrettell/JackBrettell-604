using UnityEngine;
using System.Collections;

public class LIghtFlicker : MonoBehaviour
{
    private Light childLight;

    [Header("Flicker Settings")]
    public float minFlickerDelay = 2f;  // Time between flickers
    public float maxFlickerDelay = 5f;
    public float flickerDuration = 0.05f; // How long the light is off

    void Start()
    {
        // Find the Light component in the children
        childLight = GetComponentInChildren<Light>();

        if (childLight == null)
        {
            Debug.LogWarning("No child Light found on this GameObject.");
            return;
        }

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(waitTime);

            // Flicker off briefly
            childLight.enabled = false;
            yield return new WaitForSeconds(flickerDuration);
            childLight.enabled = true;
        }
    }
}
