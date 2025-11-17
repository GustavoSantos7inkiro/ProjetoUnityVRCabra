using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [Header("References")]
    public Image fadeImage; // assign in inspector (uma Image preta cobrindo a tela)

    [Header("Options")]
    public bool fadeInOnStart = true;
    public float fadeInDuration = 0.8f;

    private void Start()
    {
        if (fadeInOnStart && fadeImage != null)
        {
            // inicia FadeIn a partir do alpha atual
            StartCoroutine(FadeIn(fadeInDuration));
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("[ScreenFader] FadeOut called but fadeImage is null.");
            yield break;
        }

        float start = fadeImage.color.a;
        float t = 0f;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, 1f, t / Mathf.Max(0.0001f, duration));
            c.a = Mathf.Clamp01(alpha);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
    }

    public IEnumerator FadeIn(float duration)
    {
        if (fadeImage == null)
        {
            Debug.LogWarning("[ScreenFader] FadeIn called but fadeImage is null.");
            yield break;
        }

        float start = fadeImage.color.a;
        float t = 0f;
        Color c = fadeImage.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(start, 0f, t / Mathf.Max(0.0001f, duration));
            c.a = Mathf.Clamp01(alpha);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
    }
}