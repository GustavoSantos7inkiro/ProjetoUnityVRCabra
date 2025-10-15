using UnityEngine;

public class CronometroSeguindo : MonoBehaviour
{
    public Transform playerHead;      // Referência à câmera do XR Origin (os olhos do jogador)
    public Vector3 offset = new Vector3(0.3f, 0.25f, 0.5f); // Offset em relação aos olhos (X= direita, Y= acima, Z= na frente)

    void LateUpdate()
    {
        if (playerHead != null)
        {
            // Posiciona o cronômetro com offset em relação à cabeça
            transform.position = playerHead.position + playerHead.rotation * offset;

            // Faz o cronômetro olhar sempre para o jogador
            transform.rotation = Quaternion.LookRotation(transform.position - playerHead.position);
        }
    }
}
