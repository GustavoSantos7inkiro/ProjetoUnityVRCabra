using UnityEngine;

public class SlotTemporario : MonoBehaviour
{
    [Header("Referências")]
    public Transform pontoEncaixe;     // onde o objeto deve ficar encaixado
    public GameObject objetoLaranja;   // objeto que vai desaparecer
    public GameObject paredeInvisivel; // box collider que deve sumir

    [Header("Configurações")]
    public float tempoAntesDeSumir = 3f;

    private bool ativado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ativado) return;

        // Verifica se é um objeto válido (pode por tag se quiser)
        if (other.CompareTag("ObjetoEspecial")) 
        {
            ativado = true;

            // Travar o objeto no ponto exato
            other.transform.SetParent(null);
            other.transform.position = pontoEncaixe.position;
            other.transform.rotation = pontoEncaixe.rotation;

            // Impede de cair ou ser movido
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            // Inicia contagem para sumir
            StartCoroutine(SumirDepois(other.gameObject));
        }
    }

    private System.Collections.IEnumerator SumirDepois(GameObject objeto)
    {
        yield return new WaitForSeconds(tempoAntesDeSumir);

        // Some com o objeto encostado
        if (objeto != null)
            Destroy(objeto);

        // Some com o objeto laranja
        if (objetoLaranja != null)
            Destroy(objetoLaranja);

        // Some com a parede invisível
        if (paredeInvisivel != null)
            Destroy(paredeInvisivel);

        // Some com o próprio slot também, se quiser:
        // Destroy(gameObject);
    }
}
