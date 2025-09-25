using UnityEngine;

public class SlotFusivel : MonoBehaviour
{
    private bool ocupado = false;
    private Disjuntor disjuntor;

    [Header("Feedback opcional")]
    public AudioSource somEncaixe; // som de encaixe, se quiser usar

    private void Start()
    {
        disjuntor = GetComponentInParent<Disjuntor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ocupado && other.CompareTag("Fusivel"))
        {
            Fusivel fusivel = other.GetComponent<Fusivel>();
            if (fusivel != null && fusivel.colocado) return;

            // encaixa fusível na posição/rotação do slot
            fusivel.transform.position = transform.position;
            fusivel.transform.rotation = transform.rotation;

            // desativa física
            Rigidbody rb = fusivel.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // NÃO faz SetParent! Mantém o fusível independente para não amassar
            // fusivel.transform.SetParent(transform); <-- removido

            // mantém a escala original do fusível
            // fusivel.transform.localScale permanece a do prefab

            // marca como ocupado
            ocupado = true;
            if (fusivel != null) fusivel.colocado = true;

            // conta no disjuntor
            disjuntor.ContarFusivel();

            // toca som de encaixe se houver
            if (somEncaixe != null)
                somEncaixe.Play();

            Debug.Log("Fusível colocado no slot!");
        }
    }
}
