using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotPuzzle : MonoBehaviour
{
    [Header("ID do Slot")]
    [Tooltip("Número identificador do slot. Deve ser igual ao ID da peça correspondente.")]
    public int idSlot;

    private bool preenchido = false;

    [Header("Manager do Puzzle")]
    [Tooltip("Arraste aqui o objeto que possui o script PuzzleManager.")]
    public PuzzleManager manager;

    [Header("Transform Final da Peça")]
    [Tooltip("Posição final da peça (em coordenadas de mundo).")]
    public Vector3 correctPosition;

    [Tooltip("Rotação final da peça (em Euler, convertida automaticamente).")]
    public Vector3 correctEulerRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        PecaPuzzle peca = other.GetComponent<PecaPuzzle>();
        if (peca != null && peca.idPeca == idSlot)
        {
            // Move e rotaciona a peça para a posição correta no mundo
            other.transform.position = correctPosition;
            other.transform.rotation = Quaternion.Euler(correctEulerRotation);

            // Desativa o XRGrabInteractable (para não poder pegar novamente)
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            // Ajusta Rigidbody para travar a peça
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            preenchido = true;

            // Conta acerto no PuzzleManager
            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"✅ Peça {peca.idPeca} encaixada no slot {idSlot} na posição {correctPosition} e rotação {correctEulerRotation}!");
        }
    }
}