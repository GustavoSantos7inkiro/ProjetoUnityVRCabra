using UnityEngine;

public class SlotPuzzle : MonoBehaviour
{
    public int idSlot; // define no inspector (10, 20, 30...)
    private bool preenchido = false;
    private PuzzleManager manager;

    private void Start()
    {
        manager = GetComponentInParent<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!preenchido && other.GetComponent<PecaPuzzle>() != null)
        {
            PecaPuzzle peca = other.GetComponent<PecaPuzzle>();

            if (peca.idPeca == idSlot) // checa se a peça corresponde ao slot
            {
                // trava a peça no slot
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;

                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb) rb.isKinematic = true;

                preenchido = true;
                manager.ContarAcerto();
                Debug.Log("Peça correta encaixada!");
            }
        }
    }
}
