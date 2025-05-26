using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1.0f;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        fadeScreen.gameObject.SetActive(true);

        Color currentColor = fadeScreen.color;
        float alphaDifference = Mathf.Abs(currentColor.a - targetAlpha);

        while (alphaDifference > 0.005f)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = currentColor;

            alphaDifference = Mathf.Abs(currentColor.a - targetAlpha);

            yield return null;
        }

        fadeScreen.gameObject.SetActive(targetAlpha != 0);
    }   
}
