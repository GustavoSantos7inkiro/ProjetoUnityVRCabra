using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotManagerTotem : MonoBehaviour
{
    [Header("Slots disponíveis (ex: slot 2 e 3)")]
    public Transform[] slotsDisponiveis;

    [Header("Índices de slots específicos")]
    public int indiceSlotCofre = 0;
    public int indiceSlotPinturas = 1;

    [Header("Luzes guia")]
    public Light luzGuiaCofre;
    public Light luzGuiaPinturas;

    [Header("Partículas")]
    public GameObject prefabParticula;
    public float duracaoDeslizamento = 0.3f;

    private List<GameObject> totensEncaixados = new List<GameObject>();
    private GameObject particulasCofreAtivas;
    private GameObject particulasPinturasAtivas;

    // ===============================================================
    public void AtivarGuiasCofre()
    {
        if (luzGuiaCofre) luzGuiaCofre.enabled = true;

        if (particulasCofreAtivas) Destroy(particulasCofreAtivas);

        Transform slot = slotsDisponiveis[indiceSlotCofre];

        particulasCofreAtivas = Instantiate(prefabParticula, slot.position, Quaternion.Euler(-90, 0, 0));
        particulasCofreAtivas.transform.SetParent(slot);
    }

    // ===============================================================
    public void AtivarGuiasPinturas()
    {
        if (luzGuiaPinturas) luzGuiaPinturas.enabled = true;

        if (particulasPinturasAtivas) Destroy(particulasPinturasAtivas);

        Transform slot = slotsDisponiveis[indiceSlotPinturas];

        particulasPinturasAtivas = Instantiate(prefabParticula, slot.position, Quaternion.Euler(-90, 0, 0));
        particulasPinturasAtivas.transform.SetParent(slot);
    }

    // ===============================================================
    public void TotemEncaixado(GameObject totem)
    {
        if (totensEncaixados.Contains(totem)) return;
        totensEncaixados.Add(totem);

        Rigidbody rb = totem.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Transform slotDestino = ObterSlotMaisProximo(totem.transform.position);


        Debug.Log("Totem encaixado. Slot destino = " + slotDestino.name);
        Debug.Log("Slot Cofre = " + slotsDisponiveis[indiceSlotCofre].name);
        Debug.Log("Slot Pinturas = " + slotsDisponiveis[indiceSlotPinturas].name);

        Debug.Log("Distancia para slot Cofre = " +
         Vector3.Distance(slotDestino.position, slotsDisponiveis[indiceSlotCofre].position));

        Debug.Log("Distancia para slot Pinturas = " +
         Vector3.Distance(slotDestino.position, slotsDisponiveis[indiceSlotPinturas].position));

        StartCoroutine(DeslizarTotem(totem, slotDestino.position, slotDestino.rotation));

       // ===== apagar partículas corretas (agora por DISTÂNCIA) =====
float distCofre = Vector3.Distance(slotDestino.position, slotsDisponiveis[indiceSlotCofre].position);
float distPinturas = Vector3.Distance(slotDestino.position, slotsDisponiveis[indiceSlotPinturas].position);

if (distCofre < 0.15f) // tolerância de 15 cm
{
    if (luzGuiaCofre) luzGuiaCofre.enabled = false;
    if (particulasCofreAtivas) Destroy(particulasCofreAtivas);
}
else if (distPinturas < 0.15f)
{
    if (luzGuiaPinturas) luzGuiaPinturas.enabled = false;
    if (particulasPinturasAtivas) Destroy(particulasPinturasAtivas);
}
    }

    // ===============================================================
    private Transform ObterSlotMaisProximo(Vector3 pos)
    {
        Transform melhor = null;
        float menorDist = float.MaxValue;

        foreach (Transform slot in slotsDisponiveis)
        {
            float d = Vector3.Distance(pos, slot.position);
            if (d < menorDist)
            {
                menorDist = d;
                melhor = slot;
            }
        }
        return melhor;
    }

    // ===============================================================
    private IEnumerator DeslizarTotem(GameObject totem, Vector3 posFinal, Quaternion rotFinal)
    {
        Vector3 posInicial = totem.transform.position;
        Quaternion rotInicial = totem.transform.rotation;

        float tempo = 0f;

        while (tempo < duracaoDeslizamento)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoDeslizamento;

            totem.transform.position = Vector3.Lerp(posInicial, posFinal, t);
            totem.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);

            yield return null;
        }

        totem.transform.position = posFinal;
        totem.transform.rotation = rotFinal;
    }

    // ===============================================================
    public void AtivarGuias() => AtivarGuiasCofre();
}