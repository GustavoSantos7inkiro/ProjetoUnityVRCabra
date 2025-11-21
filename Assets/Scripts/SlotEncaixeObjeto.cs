using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotEncaixeObjeto : MonoBehaviour
{
    [Header("Referências (originais)")]
    public GameObject objetoLaranja;   
    public GameObject paredeInvisivel; 
    [Tooltip("Tag do objeto que deve encaixar no slot")]
    public string tagObjetoValido = "ObjetoEspecial";

    [Header("Novas referências adicionais")]
    public GameObject luzParaAcender;     // Luz que liga quando encaixar
    public GameObject imagemParaEsconder; // Imagem que some quando encaixar

    [Tooltip("Objetos laranja extras para este slot")]
    public GameObject[] objetosLaranjaExtras;

    [Tooltip("Paredes invisíveis extras para este slot")]
    public GameObject[] paredesInvisiveisExtras;

    [Header("Tempos e animação")]
    public float duracaoDeslize = 1f;  
    public float tempoEspera = 3f;     

    [Header("World Transform final do objeto")]
    public Vector3 posFinal = new Vector3(18.0f, 1.22f, -4.18f);
    public Quaternion rotFinal = new Quaternion(-0.70694786f, -0.014993576f, -0.014993577f, 0.70694774f);

    private bool encaixado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (encaixado) return;

        if (!other.CompareTag(tagObjetoValido)) return;

        StartCoroutine(DeslizarEDestruir(other.gameObject));
    }

    private IEnumerator DeslizarEDestruir(GameObject objeto)
    {
        encaixado = true;

        XRGrabInteractable grab = objeto.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            if (grab.isSelected)
            {
                grab.interactionManager.CancelInteractableSelection(grab);
            }
            grab.enabled = false;
        }

        Rigidbody rb = objeto.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Collider col = objeto.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Vector3 posInicial = objeto.transform.position;
        Quaternion rotInicial = objeto.transform.rotation;

        float tempo = 0f;

        while (tempo < duracaoDeslize)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoDeslize;

            objeto.transform.position = Vector3.Lerp(posInicial, posFinal, t);
            objeto.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);

            yield return null;
        }

        objeto.transform.position = posFinal;
        objeto.transform.rotation = rotFinal;

        yield return new WaitForSeconds(tempoEspera);

        // -------------------------------
        //  NOVAS FUNÇÕES AQUI 
        // -------------------------------

        // Acender luz
        if (luzParaAcender != null)
            luzParaAcender.SetActive(true);

        // Esconder imagem
        if (imagemParaEsconder != null)
            imagemParaEsconder.SetActive(false);

        // Destruir objetos laranjas extras
        foreach (var obj in objetosLaranjaExtras)
            if (obj != null) Destroy(obj);

        // Destruir paredes invisíveis extras
        foreach (var parede in paredesInvisiveisExtras)
            if (parede != null) Destroy(parede);

        // -------------------------------
        //  FUNÇÕES ORIGINAIS DO SEU SCRIPT
        // -------------------------------

        if (objetoLaranja != null) Destroy(objetoLaranja);
        if (paredeInvisivel != null) Destroy(paredeInvisivel);

        Destroy(objeto);
    }
}