using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotPuzzle : MonoBehaviour
{
    [Header("ID do slot")]
    public int idSlot; // define no inspector (ex: 10, 20, 30)
    private bool preenchido = false;

    [Header("Manager do puzzle")]
    public PuzzleManager manager; // arraste o PuzzleManager aqui no inspector

    [Header("Transform final da peça")]
    public Vector3 correctPosition;         // posição final da peça
    public Quaternion correctRotation;      // rotação final da peça

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        PecaPuzzle peca = other.GetComponent<PecaPuzzle>();
        if (peca != null && peca.idPeca == idSlot)
        {
            // Move a peça para a posição e rotação corretas
            other.transform.position = correctPosition;
            other.transform.rotation = correctRotation;

            // Desativa a interação se tiver XR Grab Interactable
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                grab.enabled = false;
            }

            // Ajusta Rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // trava a peça
            }

            preenchido = true;

            // Conta acerto no manager
            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"Peça {peca.idPeca} encaixada no slot {idSlot}!");
        }
    }
}