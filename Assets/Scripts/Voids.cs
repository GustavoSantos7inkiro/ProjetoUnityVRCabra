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

    // Rotação exata do JSON, aplicada via código
    private Quaternion correctRotation;

    private void Awake()
    {
        // Configura a rotação de cada slot conforme o ID da pintura
        switch (idSlot)
        {
            case 13:
                correctPosition = new Vector3(11.024384f, 1.941682f, -8.654334f);
                correctRotation = new Quaternion(0.0108339237f, -0.0096537312f, -0.0031061104f, 0.9998899102f);
                break;
            case 23:
                correctPosition = new Vector3(11.054f, 1.976f, -9.133f);
                correctRotation = new Quaternion(0.007678f, -0.004341f, 0.008497f, 0.999925f);
                break;
            case 32:
                correctPosition = new Vector3(12.64438438f, 1.945682287f, -6.442334175f);
                correctRotation = new Quaternion(0.002937767f, -0.702146947f, -0.00347617f, 0.7120176f);
                break;
            default:
                Debug.LogWarning($"Slot {idSlot} não tem posição/rotação definida!");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        PecaPuzzle peca = other.GetComponent<PecaPuzzle>();
        if (peca != null && peca.idPeca == idSlot)
        {
            // Remove o parent temporariamente para manter world transform
            other.transform.SetParent(null, true);

            // Move e rotaciona a peça para a posição correta no mundo usando Quaternion direto
            other.transform.position = correctPosition;
            other.transform.rotation = correctRotation;

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

            Debug.Log($"✅ Peça {peca.idPeca} encaixada no slot {idSlot} na posição {correctPosition} e rotação {correctRotation}!");
        }
    }
}