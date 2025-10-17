using UnityEngine;
using TMPro;

public class CofreControllerXR : MonoBehaviour
{
    [Header("Partes do Cofre")]
    public Transform door;                   // Porta que vai abrir
    public TextMeshProUGUI displayText;          // Texto do display

    [Header("Botões do Cofre")]
    public CofreBotaoXR[] botoes;            // Referência aos 8 botões

    [Header("Código do Cofre")]
    public string code = "531";              // Código correto

    [Header("Rotação da Porta")]
    public float rotationSpeed = 90f;        // Velocidade da porta
    public float rotationAngle = 90f;        // Ângulo que a porta abre

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

        // Liga os botões a este cofre
        foreach (var botao in botoes)
        {
            if (botao != null)
                botao.Configurar(this);
        }
    }

    public void PressButton(string number)
{
    Debug.Log("PressButton chamado com: " + number);

    if (isOpen) return;

    currentInput += number;
    if (displayText != null)
        displayText.text = currentInput;

    if (currentInput.Length == code.Length)
    {
        if (currentInput == code)
        {
            isOpen = true;
            if (displayText != null) displayText.text = "";
            currentInput = "";
            Debug.Log("Cofre aberto!");
        }
        else
        {
            if (displayText != null) displayText.text = "Error";
            currentInput = "";
            Debug.Log("Código errado");
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
