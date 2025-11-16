using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class SceneTrigger : MonoBehaviour
{
    public string sceneName;     
    public float delay = 2f;     
    public ScreenFader fader;    

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        // 1 - Fade out
        if (fader != null)
            yield return StartCoroutine(fader.FadeOut(delay));

        // 2 - Troca de cena
        SceneManager.LoadScene(sceneName);
    }
}