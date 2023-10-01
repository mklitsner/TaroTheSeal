using System.Collections;
using System.Collections.Generic;
using AmazingAssets.AdvancedDissolve;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class City : MonoBehaviour
{
   public UnityEvent OnCityStart;
   public AdvancedDissolvePropertiesController advancedDissolvePropertiesController;

    private void Start()
    {
        OnCityStart.Invoke();
        StartCoroutine(LerpToZero());
    }

    private IEnumerator LerpToZero()
    {
        float elapsedTime = 0f;
        float duration = 1f;
        float startValue = 1f;
        float endValue = 0f;

        while (elapsedTime < duration)
        {
            advancedDissolvePropertiesController.cutoutStandard.clip = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        advancedDissolvePropertiesController.cutoutStandard.clip = endValue; // Ensure the value is exactly 0 after the coroutine
    }
}
