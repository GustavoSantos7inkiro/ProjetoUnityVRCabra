using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    public int totalSlots = 6;          // Quantas peças precisam ser encaixadas
    private int acertos = 0;

    [Header("Recompensas")]
    public Light rewardLight;           // Luz que acende ao completar o puzzle
    public GameObject caixa;            // Caixa que será ativada
    public GameObject tampaCaixa;       // Tampa que vai sumir

    [Header("SlotManagerTotem para luz guia")]
    public SlotManagerTotem slotManager;

    public void ContarAcerto()
    {
        acertos++;
        Debug.Log("Peça correta encaixada! Total: " + acertos + "/" + totalSlots);

        if (acertos >= totalSlots)
        {
            Debug.Log("Quebra-cabeça resolvido!");

            if (rewardLight != null)
                rewardLight.enabled = true;

            if (caixa != null)
                caixa.SetActive(true);

            if (tampaCaixa != null)
                tampaCaixa.SetActive(false);

            // Acende a luz guia da mesa que depende do puzzle
            if (slotManager != null && slotManager.luzGuiaPinturas != null)
                slotManager.luzGuiaPinturas.enabled = true;
        }
    }
}