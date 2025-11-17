using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTrigger : MonoBehaviour
{
    [Header("Configuração de Cena")]
    public string sceneName;
    public float delay = 1f;
    public ScreenFader fader; // pode ficar null, o script tentará encontrar um automaticamente

    [Header("Opções")]
    public bool requirePlayerTag = false;
    public string playerTag = "Player";

    private bool sceneLoading = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SceneTrigger] OnTriggerEnter by '{other.name}' (tag='{other.tag}')");

        if (requirePlayerTag && !other.CompareTag(playerTag))
        {
            Debug.Log("[SceneTrigger] Ignored: does not have Player tag.");
            return;
        }

        if (sceneLoading)
        {
            Debug.Log("[SceneTrigger] Already loading, ignoring.");
            return;
        }

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[SceneTrigger] sceneName is empty! Set the exact scene name (Build Settings).");
            return;
        }

        sceneLoading = true;
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        // try to find a fader if none assigned
        if (fader == null)
        {
            fader = FindObjectOfType<ScreenFader>();
            Debug.Log($"[SceneTrigger] fader was null — FindObjectOfType returned: {(fader != null ? fader.name : "null")}");
        }

        if (fader != null)
        {
            Debug.Log("[SceneTrigger] Starting FadeOut...");
            yield return StartCoroutine(fader.FadeOut(delay));
            Debug.Log("[SceneTrigger] FadeOut complete.");
        }
        else
        {
            // no fader, small safe wait so logs appear and frame can update
            yield return null;
        }

        Debug.Log("[SceneTrigger] Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}