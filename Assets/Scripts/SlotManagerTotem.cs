using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotManagerTotem : MonoBehaviour
{
    [Header("Slots disponíveis (slot 2 e 3)")]
    public Transform[] slotsDisponiveis;

    [Header("Luzes e partículas")]
    public Light[] luzesGuias;
    public GameObject prefabParticula;
    public float duracaoDeslizamento = 0.3f;

    private List<GameObject> totensEncaixados = new List<GameObject>();

    public void AtivarGuias()
    {
        // Ativa luzes e partículas para mostrar onde encaixar
        foreach (Light luz in luzesGuias)
            luz.enabled = true;

        foreach (Transform slot in slotsDisponiveis)
        {
            if (prefabParticula != null)
            {
                GameObject particula = Instantiate(prefabParticula, slot.position, Quaternion.identity);
                Destroy(particula, 2f); // Dura 2 segundos
            }
        }
    }

    public void EncaixarTotem(GameObject totem)
    {
        // Verifica se já foi encaixado
        if (totensEncaixados.Contains(totem)) return;

        // Encontra o primeiro slot livre
        Transform slotLivre = null;
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
            {
                slotLivre = s;
                break;
            }
        }

        if (slotLivre == null) return; // nenhum slot livre

        totensEncaixados.Add(totem);

        // Desativa física temporariamente
        Rigidbody rb = totem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;      // antes: rb.velocity
            rb.angularVelocity = Vector3.zero;     // continua igual
        }

        // Inicia deslize para o slot
        StartCoroutine(DeslizarTotem(totem, slotLivre.position, slotLivre.rotation));

        // Desliga luzes do slot após encaixe
        foreach (Light luz in luzesGuias)
        {
            luz.enabled = false;
        }
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
