using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotPuzzle : MonoBehaviour
{
    [Header("ID do slot")]
    public int idSlot; // define no inspector (ex: 10, 20, 30)
    private bool preenchido = false;

    [Header("Manager do puzzle")]
    public PuzzleManager manager; // arraste o PuzzleManager aqui no inspector

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        PecaPuzzle peca = other.GetComponent<PecaPuzzle>();
        if (peca != null && peca.idPeca == idSlot)
        {
            // Encaixa a peça na posição do slot
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            // Se tiver XR Grab Interactable, desativa a interação
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

            // Contar acerto
            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"Peça {peca.idPeca} encaixada no slot {idSlot}!");
        }
    }
}