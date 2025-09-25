using UnityEngine;

public class SlotFusivel : MonoBehaviour
{
    private bool ocupado = false;   // se j� tem um fus�vel aqui
    private Disjuntor disjuntor;    // refer�ncia ao disjuntor principal

    private void Start()
    {
        disjuntor = GetComponentInParent<Disjuntor>();
    }

   private void OnTriggerEnter(Collider other)
{
    if (!ocupado && other.CompareTag("Fusivel"))
    {
        Fusivel fusivel = other.GetComponent<Fusivel>();

        // já foi colocado em outro slot?
        if (fusivel != null && fusivel.colocado) return;

        // fixa o fusível no slot
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        ocupado = true;
        if (fusivel != null) fusivel.colocado = true;

        disjuntor.ContarFusivel();

        Debug.Log("Fusível colocado no slot!");
    }
}
}

