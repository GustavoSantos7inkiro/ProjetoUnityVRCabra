using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    public int totalSlots = 6;          // Quantas peças precisam ser encaixadas
    private int acertos = 0;            // Contador de peças corretas

    [Header("Recompensas")]
    public Light rewardLight;           // A luz que será acesa ao completar o puzzle
    public GameObject caixa;            // Caixa que será aberta ao concluir o puzzle (opcional)

    // Chamado pelos Slots quando uma peça correta é encaixada
    public void ContarAcerto()
    {
        acertos++;

        Debug.Log("Peça correta encaixada! Total: " + acertos + "/" + totalSlots);

        if (acertos >= totalSlots)
        {
            Debug.Log("Quebra-cabeça resolvido!");

            // Acende a luz
            if (rewardLight != null)
                rewardLight.enabled = true;

            // Abre a caixa ou ativa outro objeto
            if (caixa != null)
                caixa.SetActive(true);
        }
    }
}