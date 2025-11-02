using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotManagerTotem : MonoBehaviour
{
    [Header("Slots disponíveis (slot 2 e 3)")]
    public Transform[] slotsDisponiveis;

    [Header("Luzes guia")]
    public Light luzGuiaCofre;      // acende quando o cofre abrir
    public Light luzGuiaPinturas;   // acende quando puzzle das pinturas for concluído

    [Header("Partículas")]
    public GameObject prefabParticula;
    public float duracaoDeslizamento = 0.3f;

    private List<GameObject> totensEncaixados = new List<GameObject>();

    // Ativa luzes e partículas quando o cofre abre
    public void AtivarGuias()
    {
        if (luzGuiaCofre != null)
            luzGuiaCofre.enabled = true;

        foreach (Transform slot in slotsDisponiveis)
        {
            if (prefabParticula != null)
            {
                GameObject particula = Instantiate(prefabParticula, slot.position, Quaternion.identity);
                Destroy(particula, 2f);
            }
        }
    }

    // Quando um totem é encaixado
    public void TotemEncaixado(GameObject totem)
    {
        if (totensEncaixados.Contains(totem)) return;

        totensEncaixados.Add(totem);

        // Desativa física temporariamente
        Rigidbody rb = totem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Move o totem suavemente para o slot
        Transform slotLivre = ObterSlotLivre();
        if (slotLivre != null)
            StartCoroutine(DeslizarTotem(totem, slotLivre.position, slotLivre.rotation));
    }

    private Transform ObterSlotLivre()
    {
        foreach (Transform s in slotsDisponiveis)
        {
            bool ocupado = false;
            foreach (GameObject t in totensEncaixados)
            {
                if (Vector3.Distance(t.transform.position, s.position) < 0.01f)
                {
                    ocupado = true;
                    break;
                }
            }
            if (!ocupado)
                return s;
        }
        return null;
    }

    private IEnumerator DeslizarTotem(GameObject totem, Vector3 posFinal, Quaternion rotFinal)
    {
        Vector3 posInicial = totem.transform.position;
        Quaternion rotInicial = totem.transform.rotation;
        float tempo = 0f;

        while (tempo < duracaoDeslizamento)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoDeslizamento);
            totem.transform.position = Vector3.Lerp(posInicial, posFinal, t);
            totem.transform.rotation = Quaternion.Slerp(rotInicial, rotFinal, t);
            yield return null;
        }

        totem.transform.position = posFinal;
        totem.transform.rotation = rotFinal;
    }
}