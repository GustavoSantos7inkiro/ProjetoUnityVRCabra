using UnityEngine; 
using TMPro;

public class CofreController : MonoBehaviour
{
    [Header("Partes do Cofre")]
    public Transform door;                // Porta que vai abrir
    public TextMeshPro displayText;       // Texto do display

    [Header("Código do Cofre")]
    public string code = "531";           // Código correto

    [Header("Rotação da Porta")]
    public float rotationSpeed = 90f;     // Velocidade da porta
    public float rotationAngle = 90f;     // Ângulo que a porta abre

    [Header("Slot Manager Totem")]
    public SlotManagerTotem slotManager;  // Referência ao SlotManager

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
            {
                isOpen = true;

                //  Limpa o display assim que o código estiver correto
                if (displayText != null)
                    displayText.text = "";

                currentInput = "";

                // Chama o SlotManagerTotem para ativar luzes guia e partículas
                if (slotManager != null)
                    slotManager.AtivarGuias();
            }
            else
            {
                if (displayText != null)
                    displayText.text = "Error";
                currentInput = "";
            }
        }
    }

    void Update()
    {
        if (isOpen && door != null)
        {
            door.rotation = Quaternion.RotateTowards(door.rotation, openRotation, rotationSpeed * Time.deltaTime);
        }

        // Teste no editor (teclado)
        if (Application.isEditor && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) PressButton("1");
            if (Input.GetKeyDown(KeyCode.Alpha2)) PressButton("2");
            if (Input.GetKeyDown(KeyCode.Alpha3)) PressButton("3");
            if (Input.GetKeyDown(KeyCode.Alpha4)) PressButton("4");
            if (Input.GetKeyDown(KeyCode.Alpha5)) PressButton("5");
            if (Input.GetKeyDown(KeyCode.Alpha6)) PressButton("6");
            if (Input.GetKeyDown(KeyCode.Alpha7)) PressButton("7");
            if (Input.GetKeyDown(KeyCode.Alpha8)) PressButton("8");
        }
    }
}