using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
    public ScreenFader fader;
    public float duration = 1f;

    void Start()
    {
        StartCoroutine(fader.FadeIn(duration));
    }
}

