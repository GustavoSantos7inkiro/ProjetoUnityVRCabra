using UnityEngine;
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

    private GameObject particulasCofreAtivas;
    private GameObject particulasPinturasAtivas;

    // ===============
    public void AtivarGuiasCofre()
    {
        if (luzGuiaCofre) luzGuiaCofre.enabled = true;

        if (particulasCofreAtivas) Destroy(particulasCofreAtivas);

        Transform slot = slotsDisponiveis[indiceSlotCofre];

        particulasCofreAtivas = Instantiate(prefabParticula, slot.position, Quaternion.Euler(-90, 0, 0));
        particulasCofreAtivas.transform.SetParent(slot);
    }

    // ===========
    public void AtivarGuiasPinturas()
    {
        if (luzGuiaPinturas) luzGuiaPinturas.enabled = true;

        if (particulasPinturasAtivas) Destroy(particulasPinturasAtivas);

        Transform slot = slotsDisponiveis[indiceSlotPinturas];

        particulasPinturasAtivas = Instantiate(prefabParticula, slot.position, Quaternion.Euler(-90, 0, 0));
        particulasPinturasAtivas.transform.SetParent(slot);
    }

    // =======================
    //  ESTE MÉTODO AGORA APENAS DESLIGA LUZES E PARTÍCULAS
    public void TotemEncaixado(GameObject totem)
    {
        // Distâncias para decidir qual slot foi ativado
        float distCofre = Vector3.Distance(totem.transform.position, slotsDisponiveis[indiceSlotCofre].position);
        float distPinturas = Vector3.Distance(totem.transform.position, slotsDisponiveis[indiceSlotPinturas].position);

        Debug.Log($" SlotManager recebeu Totem: {totem.name} DistCofre {distCofre} DistPinturas {distPinturas}");

        // Se encaixou no slot do cofre
        if (distCofre < distPinturas)
        {
            if (luzGuiaCofre) luzGuiaCofre.enabled = false;
            if (particulasCofreAtivas) Destroy(particulasCofreAtivas);
        }
        else
        {
            if (luzGuiaPinturas) luzGuiaPinturas.enabled = false;
            if (particulasPinturasAtivas) Destroy(particulasPinturasAtivas);
        }
    }

    // ===================
    public void AtivarGuias() => AtivarGuiasCofre();
}