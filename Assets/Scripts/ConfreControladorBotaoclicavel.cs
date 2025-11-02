using UnityEngine;
using TMPro;

public class CofreControllerXR : MonoBehaviour
{
    [Header("Partes do Cofre")]
    public Transform door;
    public TextMeshProUGUI displayText;

    [Header("Botões do Cofre")]
    public CofreBotaoXR[] botoes;

    [Header("Código do Cofre")]
    public string code = "531";

    [Header("Rotação da Porta")]
    public float rotationSpeed = 90f;
    public float rotationAngle = 90f;

    [Header("SlotManagerTotem")]
    public SlotManagerTotem slotManager;

    private string currentInput = "";
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (door != null)
        {
            closedRotation = door.rotation;
            openRotation = closedRotation * Quaternion.Euler(0, rotationAngle, 0);
        }

        if (displayText != null)
            displayText.text = "";

        foreach (var botao in botoes)
            if (botao != null) botao.Configurar(this);
    }

    public void PressButton(string number)
    {
        if (isOpen) return;

        currentInput += number;

        if (displayText != null)
            displayText.text = currentInput;

        if (currentInput.Length == code.Length)
        {
            if (currentInput == code)
                AbrirCofreAutomatico();
            else
            {
                if (displayText != null)
                    displayText.text = "Error";
                currentInput = "";
                Debug.Log("Código errado");
            }
        }
    }

    public void AbrirCofreAutomatico()
    {
        if (!isOpen)
        {
            isOpen = true;
            currentInput = "";
            if (displayText != null)
                displayText.text = "";

            Debug.Log("Cofre aberto automaticamente!");

            // Acende luz guia da mesa
            if (slotManager != null)
            {
                slotManager.AtivarGuias();
                Debug.Log("Luz guia e partículas ativadas nos slots de totem.");
            }
        }
    }

    void Update()
    {
        if (isOpen && door != null)
            door.rotation = Quaternion.RotateTowards(door.rotation, openRotation, rotationSpeed * Time.deltaTime);
    }
}