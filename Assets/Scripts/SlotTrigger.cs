using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public SlotManagerFusivel slotManager;

    private void OnTriggerEnter(Collider other)
    {
        Fusivel fusivel = other.GetComponent<Fusivel>();
        if (fusivel != null && !fusivel.colocado)
        {
            slotManager.EncaixarFusivel(fusivel);
        }
    }
}
