using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public SlotManagerFusivel slotManager; // Arraste o SlotManager no Inspector

    private void OnTriggerEnter(Collider other)
    {
        Fusivel fusivel = other.GetComponent<Fusivel>();
        if (fusivel != null && !fusivel.colocado)
        {
            slotManager.EncaixarFusivel(fusivel.gameObject);
        }
    }
}
