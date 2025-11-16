using UnityEngine;
using System.Linq;

public class KillSimulator : MonoBehaviour
{
    void Awake()
    {
        DestroyAnySimulator();
    }

    void DestroyAnySimulator()
    {
        // Procura por todos os GameObjects carregados (inclui instâncias que foram movidas para DontDestroyOnLoad)
        var all = Resources.FindObjectsOfTypeAll<GameObject>();

        int removed = 0;

        foreach (var go in all)
        {
            // Checa nomes que você viu no console (ajuste se necessário)
            if (go.name == "XR Device Simulator" || go.name == "XR Device Simulator UI")
            {
                // Só destrói se for realmente uma instância em cena (ou no container DontDestroyOnLoad)
                if (go.scene.IsValid())
                {
                    // Se você quer apenas desativar em vez de destruir, troque por go.SetActive(false);
                    Destroy(go);
                    Debug.Log($"[KillSimulator] Destroyed instance: {go.name}");
                    removed++;
                }
                else
                {
                    Debug.Log($"[KillSimulator] Skipped asset/prefab: {go.name}");
                }
            }
        }

        Debug.Log($"[KillSimulator] Removed {removed} object(s).");
    }
}