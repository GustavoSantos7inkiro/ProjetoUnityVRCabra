using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit; // necessário para XRGrabInteractable

public class SlotEncaixeObjeto : MonoBehaviour
{
    [Header("Referências")]
    public GameObject objetoLaranja;   // Objeto laranja que desaparece junto
    public GameObject paredeInvisivel; // Parede invisível que desaparece junto
    [Tooltip("Tag do objeto que deve encaixar no slot")]
    public string tagObjetoValido = "ObjetoEspecial";

    [Header("Tempos e animação")]
    public float duracaoDeslize = 1f;  // Tempo que o objeto leva para deslizar até o slot
    public float tempoEspera = 3f;     // Tempo que o objeto fica encaixado antes de desaparecer

    [Header("World Transform final do objeto")]
    public Vector3 posFinal = new Vector3(18.0f, 1.22f, -4.18f);
    public Quaternion rotFinal = new Quaternion(-0.70694786f, -0.014993576f, -0.014993577f, 0.70694774f);

    private bool encaixado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (encaixado) return;

        // Verifica se o objeto que entrou é o correto
        if (!other.CompareTag(tagObjetoValido)) return;

        StartCoroutine(DeslizarEDestruir(other.gameObject));
    }

    private IEnumerator DeslizarEDestruir(GameObject objeto)
    {
        encaixado = true;

        // Se o objeto tiver XRGrabInteractable, solta da mão e desativa pegar de novo
        XRGrabInteractable grab = objeto.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            if (grab.isSelected)
            {
                // força soltar da mão
                grab.interactionManager.CancelInteractableSelection(grab);
            }
            grab.enabled = false;
        }

        // Desativa física para não ser empurrado
        Rigidbody rb = objeto.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Desativa collider para não bater em nada durante o deslize
        Collider col = objeto.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Vector3 posInicial = objeto.transform.position;
        Quaternion rotInicial = objeto.transform.rotation;

        float tempo = 0f;

        // Desliza até a posição final
        while (tempo < duracaoDeslize)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoDeslize;

            objeto.transform.position = Vector3.Lerp(posInicial, posFinal, t);
            objeto.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);

            yield return null;
        }

        // Garante posição final exata
        objeto.transform.position = posFinal;
        objeto.transform.rotation = rotFinal;

        // Espera o tempo de encaixe
        yield return new WaitForSeconds(tempoEspera);

        // Destrói primeiro o laranja e a parede, depois o objeto encaixado
        if (objetoLaranja != null) Destroy(objetoLaranja);
        if (paredeInvisivel != null) Destroy(paredeInvisivel);
        Destroy(objeto);
    }
}