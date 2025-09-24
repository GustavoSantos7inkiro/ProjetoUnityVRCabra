using UnityEngine;

public class SlotFusivel : MonoBehaviour
{
    private bool ocupado = false;   // se já tem um fusível aqui
    private Disjuntor disjuntor;    // referência ao disjuntor principal

    private void Start()
    {
        disjuntor = GetComponentInParent<Disjuntor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ocupado && other.CompareTag("Fusivel"))
        {
            // fixa o fusível na posição do slot
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            // desativa física (para não cair)
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = true;

            ocupado = true;
            disjuntor.ContarFusivel();

            Debug.Log("Fusível colocado no slot!");
        }
    }
}

