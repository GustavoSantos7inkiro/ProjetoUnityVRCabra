using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class SlotFusivelVR_Final2 : MonoBehaviour
{
    [Header("Configurações do encaixe")]
    public Transform pontoEncaixe; // ponto exato onde o fusível deve ficar
    public float duracaoDeslizamento = 0.3f; // tempo do deslize em segundos

    private bool ocupado = false;
    private Disjuntor disjuntor;

    private void Start()
    {
        disjuntor = GetComponentInParent<Disjuntor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ocupado) return;
        if (!other.CompareTag("Fusivel")) return;

        Fusivel fusivel = other.GetComponent<Fusivel>();
        if (fusivel == null || fusivel.colocado) return;

        // Se estiver sendo segurado, soltar
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.CancelInteractableSelection(grab);
        }

        ocupado = true;

        // Desativa física temporariamente
        Rigidbody rb = fusivel.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Guarda escala original
        Vector3 escalaOriginal = fusivel.transform.localScale;

        // Remove qualquer parent anterior para evitar deslocamento por hierarquia
        fusivel.transform.SetParent(null);

        // Inicia coroutine para deslizar suavemente até o ponto de encaixe
        StartCoroutine(DeslizarFusivel(fusivel, escalaOriginal));
    }

    private IEnumerator DeslizarFusivel(Fusivel fusivel, Vector3 escalaOriginal)
    {
        Vector3 posInicial = fusivel.transform.position;
        Quaternion rotInicial = fusivel.transform.rotation;

        Vector3 posFinal = pontoEncaixe.position;
        Quaternion rotFinal = pontoEncaixe.rotation;

        float tempo = 0f;

        while (tempo < duracaoDeslizamento)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoDeslizamento);

            fusivel.transform.position = Vector3.Lerp(posInicial, posFinal, t);
            fusivel.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);
            fusivel.transform.localScale = escalaOriginal;

            yield return null;
        }

        // Garante posição/rotação/escala final
        fusivel.transform.position = posFinal;
        fusivel.transform.rotation = rotFinal;
        fusivel.transform.localScale = escalaOriginal;

        // Faz filho do slot mantendo a posição no mundo
        fusivel.transform.SetParent(transform, true); // <== aqui é importante!

        // Marca como encaixado
        fusivel.colocado = true;

        // Conta no disjuntor
        disjuntor.ContarFusivel();

        Debug.Log("Fusível encaixado perfeitamente no slot!");
    }
}