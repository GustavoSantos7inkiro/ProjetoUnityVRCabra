using UnityEngine;
using TMPro;

public class CofreControllerXR : MonoBehaviour
{
    [Header("Partes do Cofre")]
    public Transform door; // Porta que vai abrir
    public TextMeshProUGUI displayText; // Texto do display

    [Header("Botões do Cofre")]
    public CofreBotaoXR[] botoes; // Referência aos 8 botões

    [Header("Código do Cofre")]
    public string code = "531"; // Código correto

    [Header("Rotação da Porta")]
    public float rotationSpeed = 90f;
    public float rotationAngle = 90f;

    [Header("Referência ao SlotManagerTotem")]
    public SlotManagerTotem slotManager; // <-- Arraste o objeto com o SlotManagerTotem aqui

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

        // Configura cada botão para este cofre
        foreach (var botao in botoes)
        {
            if (botao != null)
                botao.Configurar(this);
        }
    }

    // Chamado por cada botão
    public void PressButton(string number)
    {
        if (isOpen) return;

        currentInput += number;

        if (displayText != null)
            displayText.text = currentInput;

        if (currentInput.Length == code.Length)
        {
            if (currentInput == code)
            {
                AbrirCofreAutomatico();
            }
            else
            {
                if (displayText != null)
                    displayText.text = "Error";
                currentInput = "";
                Debug.Log("Código errado");
            }
        }
    }

    // Abre o cofre automaticamente (pode ser chamado pelo Disjuntor)
    public void AbrirCofreAutomatico()
    {
        if (!isOpen)
        {
            isOpen = true;
            currentInput = "";
            if (displayText != null)
                displayText.text = "";

            Debug.Log("Cofre aberto automaticamente!");

            // ✅ Ativa as luzes e partículas dos slots de totem
            if (slotManager != null)
            {
                slotManager.AtivarGuias();
                Debug.Log("Luzes guia e partículas ativadas nos slots de totem.");
            }
            else
            {
                Debug.LogWarning("SlotManagerTotem não atribuído no CofreControllerXR!");
            }
        }
    }

    void Update()
    {
        if (isOpen && door != null)
        {
            door.rotation = Quaternion.RotateTowards(door.rotation, openRotation, rotationSpeed * Time.deltaTime);
        }
    }
}