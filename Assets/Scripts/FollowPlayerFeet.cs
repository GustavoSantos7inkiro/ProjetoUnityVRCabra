using UnityEngine;
using UnityEngine.XR;

public class FollowPlayerFeet : MonoBehaviour
{
    public Transform xrCamera;

    void Update()
    {
        // Mantém o pé na mesma posição XZ da câmera, mas no chão
        Vector3 pos = xrCamera.position;
        pos.y = 0.05f;  // altura pequena acima do chão
        transform.position = pos;
    }
}