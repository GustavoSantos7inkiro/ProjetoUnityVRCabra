using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotTotem : MonoBehaviour
{
    [Header("ID do Slot")]
    [Tooltip("Número identificador do slot. Deve ser igual ao ID do totem correspondente.")]
    public int idSlot;

    public bool preenchido = false;

    [Header("Manager do Puzzle (opcional)")]
    [Tooltip("Arraste aqui o objeto que controla o puzzle geral (se houver).")]
    public PuzzleManagerTotem manager;

    [Header("Luz para acender (opcional)")]
    [Tooltip("Arraste aqui a luz que será ativada quando o Totem 3 for encaixado.")]
    public Light luzTotem3;

    private Vector3 correctPosition;
    private Quaternion correctRotation;

    private void Start()
    {
        // Configura automaticamente os valores corretos pelo idSlot
        switch (idSlot)
        {
            case 1:
                correctPosition = new Vector3(8.144384f, 1.315682f, -0.022334f);
                correctRotation = new Quaternion(-9.770124e-9f, 0.965926f, -3.64627e-8f, 0.2588185f);
                break;

            case 2:
                correctPosition = new Vector3(9.444384f, 1.315682f, -0.132334f);
                correctRotation = new Quaternion(-3.774895e-8f, 0f, 0f, 1f);
                break;

            case 3:
                correctPosition = new Vector3(8.694384f, 1.325682f, 0.987666f);
                correctRotation = new Quaternion(2.298011e-8f, 0.7933533f, -2.994826e-8f, -0.6087615f);
                break;
        }

        // Garante que a luz comece apagada
        if (luzTotem3 != null)
            luzTotem3.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (preenchido) return;

        Totem totem = other.GetComponent<Totem>();
        if (totem != null && totem.idTotem == idSlot)
        {
            // Remove parent temporariamente (mantém posição mundial)
            other.transform.SetParent(null, true);

            // Move e rotaciona o totem na posição/rotação corretas do mundo
            other.transform.position = correctPosition;
            other.transform.rotation = correctRotation;

            // Impede o totem de ser movido novamente
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null)
                grab.enabled = false;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.isKinematic = true;

            preenchido = true;

            // 🔆 Se este for o slot 3 e a luz existir, acende a luz
            if (idSlot == 3 && luzTotem3 != null)
            {
                luzTotem3.enabled = true;
                Debug.Log("💡 Luz do Totem 3 acesa!");
            }

            // Notifica o manager se houver
            if (manager != null)
                manager.ContarAcerto();

            Debug.Log($"✅ Totem {totem.idTotem} encaixado no slot {idSlot} na posição {correctPosition} e rotação {correctRotation}!");


            // ✅✅✅ Avisar o SlotManagerTotem APENAS para totens válidos (ID 2 ou 3)
            if (totem.idTotem == 2 || totem.idTotem == 3)
            {
                var slotManagerTotem = FindFirstObjectByType<SlotManagerTotem>();
                if (slotManagerTotem != null)
                    slotManagerTotem.TotemEncaixado(other.gameObject);
            }
            // ✅✅✅ FIM DA ADIÇÃO
        }
    }
}