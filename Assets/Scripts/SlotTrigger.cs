using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SlotTrigger : MonoBehaviour
{
    public SlotManagerFusivel slotManager;
    public int slotIndex; // Índice do slot neste SlotManager

    private void Reset()
    {
        // Garante que o collider seja trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Fusivel fusivel = other.GetComponent<Fusivel>();
        if (fusivel != null && !fusivel.colocado)
        {
            slotManager.EncaixarFusivelNoSlot(fusivel, slotIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Opcional: desencaixar se fusível sair do trigger
        // Fusivel fusivel = other.GetComponent<Fusivel>();
        // if (fusivel != null && fusivel.colocado)
        // {
        //     slotManager.DesencaixarFusivel(slotIndex);
        //     fusivel.colocado = false;
        // }
    }
}
