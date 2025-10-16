using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CofreBotaoXR : MonoBehaviour
{
    [Header("Número do botão")]
    public string numero; // Ex: "1", "2", etc.

    private CofreControllerXR cofre;
    private XRBaseInteractable interactable;

    public void Configurar(CofreControllerXR c)
    {
        cofre = c;
    }

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable != null)
            interactable.selectEntered.AddListener(OnBotaoPressionado);
    }

    private void OnBotaoPressionado(SelectEnterEventArgs args)
    {
        if (cofre != null)
            cofre.PressButton(numero);
    }

    void OnDestroy()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnBotaoPressionado);
    }
}
