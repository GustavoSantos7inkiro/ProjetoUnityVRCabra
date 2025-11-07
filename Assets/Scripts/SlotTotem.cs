using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class SlotTotem : MonoBehaviour
{
    [Header("ID do Slot")]
    [Tooltip("Número identificador do slot. Deve ser igual ao ID do totem correspondente.")]
    public int idSlot;

    public bool preenchido = false;

    [Header("Manager do Puzzle (opcional)")]
    [Tooltip("Arraste aqui o objeto que controla o puzzle geral (se houver).")]
    public PuzzleManagerTotem manager;

    [Header("Luz para acender (opcional)")]
    [Tooltip("Arraste aqui a luz que será ativada quando o Totem 3 for encaixado.")]
    public Light luzTotem3;

    [Header("Porta a ser aberta quando o totem 3 for colocado")]
    public GameObject porta;

    private Vector3 correctPosition;
    private Quaternion correctRotation;

    private void Start()
    {
        switch (idSlot)
        {
            case 1:
                correctPosition = new Vector3(8.144384f, 1.315682f, -0.022334f);
                correctRotation = new Quaternion(-9.770124e-9f, 0.965926f, -3.64627e-8f, 0.2588185f);
                break;

            case 2:
                correctPosition = new Vector3(9.444384f, 1.315682f, -0.132334f);
                correctRotation = new Quaternion(-3.774895e-8f, 0f, 0f, 1f);
                break;

            case 3:
                correctPosition = new Vector3(8.694384f, 1.325682f, 0.987666f);
                correctRotation = new Quaternion(2.298011e-8f, 0.7933533f, -2.994826e-8f, -0.6087615f);
                break;
        }

        if (luzTotem3 != null)
            luzTotem3.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        Totem totem = other.GetComponent<Totem>();
        if (totem != null && totem.idTotem == idSlot)
        {
            other.transform.SetParent(null, true);

            other.transform.position = correctPosition;
            other.transform.rotation = correctRotation;

            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            preenchido = true;

            // ✅ Se este for o Totem 3 → acende luz e abre a porta
            if (idSlot == 3)
            {
                if (luzTotem3 != null)
                    luzTotem3.enabled = true;

                Debug.Log("💡 Luz do Totem 3 acesa!");

                // ✅ Abra a porta aqui
                if (porta != null)
                {
                    Debug.Log("🚪 Abrindo porta (rotação Y +90°)");
                    StartCoroutine(AbrirPorta());
                }
            }

            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"✅ Totem {totem.idTotem} encaixado no slot {idSlot} na posição {correctPosition} e rotação {correctRotation}!");


            // ✅ Avisar SlotManagerTotem somente para Totem 2 e 3
            if (totem.idTotem == 2 || totem.idTotem == 3)
            {
                var slotManagerTotem = FindFirstObjectByType<SlotManagerTotem>();
                if (slotManagerTotem != null)
                    slotManagerTotem.TotemEncaixado(other.gameObject);
            }
        }
    }

    // ✅ Animação suave de abrir porta
private IEnumerator AbrirPorta()
{
    // ✅ Rotacionar o OBJETO PAI
    Transform portaTransform = porta.transform;

    Quaternion rotInicial = portaTransform.rotation;

    // ✅ Abre para fora → -90° no eixo Y
    Quaternion rotFinal = rotInicial * Quaternion.Euler(0, -90f, 0);

    float tempo = 0f;
    float duracao = 1.2f;

    while (tempo < duracao)
    {
        tempo += Time.deltaTime;

        float t = Mathf.SmoothStep(0, 1, tempo / duracao);

        portaTransform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);

        yield return null;
    }

    // ✅ Travar aberta permanentemente
    portaTransform.rotation = rotFinal;
}
}