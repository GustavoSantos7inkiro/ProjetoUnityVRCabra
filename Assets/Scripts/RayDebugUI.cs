using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

public class RayDebugUI : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor != null)
        {
            // Tenta detectar se o Ray está atingindo algo 3D
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Debug.Log("Ray atingiu 3D: " + hit.collider.name);
            }

            // Tenta detectar UI (Canvas)
            if (rayInteractor.TryGetCurrentUIRaycastResult(out RaycastResult uiResult))
            {
                if (uiResult.gameObject != null)
                {
                    Debug.Log("Ray atingiu UI: " + uiResult.gameObject.name);
                }
            }
        }
    }
}
