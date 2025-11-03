using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Fusivel : MonoBehaviour
{
    public bool colocado = false;
    public Transform fusivelVisual; // Arraste o objeto visual aqui

    private XRGrabInteractable grab;
    private Rigidbody rb;

    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    public void TravarNoSlot(Vector3 pos, Vector3 rotEuler)
    {
        colocado = true;

        // Cancelar seleção se estiver segurando
        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.CancelInteractableSelection((IXRSelectInteractable)grab);
        }

        grab.enabled = false; // Desativa XRGrab
        rb.isKinematic = true; // Desativa física
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // Move o pivot
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rotEuler);

        // Ajusta visual se necessário
        if (fusivelVisual != null)
            fusivelVisual.localRotation = Quaternion.identity;
    }
}