using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotTotem : MonoBehaviour
{
    [Header("ID do Slot")]
    [Tooltip("Número identificador do slot. Deve ser igual ao ID do totem correspondente.")]
    public int idSlot;

    private bool preenchido = false;

    [Header("Manager do Puzzle (opcional)")]
    [Tooltip("Arraste aqui o objeto que controla o puzzle geral (se houver).")]
    public PuzzleManagerTotem manager;

    [Header("Transform Final do Totem")]
    [Tooltip("Posição final do totem (em coordenadas de mundo).")]
    public Vector3 correctPosition;

    [Tooltip("Rotação final do totem (em Quaternion, use os valores do JSON do World Transform).")]
    public Quaternion correctRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        Totem totem = other.GetComponent<Totem>();
        if (totem != null && totem.idTotem == idSlot)
        {
            // Remove parent temporariamente (mantém posição mundial)
            other.transform.SetParent(null, true);

            // Move e rotaciona o totem na posição/rotação corretas do mundo
            other.transform.position = correctPosition;
            other.transform.rotation = correctRotation;

            // Impede o totem de ser movido novamente
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            preenchido = true;

            // Notifica o manager se houver
            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"✅ Totem {totem.idTotem} encaixado no slot {idSlot} na posição {correctPosition} e rotação {correctRotation}!");
        }
    }
}
