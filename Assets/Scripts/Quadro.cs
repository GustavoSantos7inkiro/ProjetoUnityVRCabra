using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    [Tooltip("Quantas peças precisam ser encaixadas")]
    public int totalSlots = 6;          // Quantas peças precisam ser encaixadas

    private int acertos = 0;            // Contador de peças corretas

    [Header("Recompensas")]
    [Tooltip("Luz que será acesa ao completar o puzzle")]
    public Light rewardLight;           // A luz que será acesa ao completar o puzzle

    [Tooltip("Caixa que será ativada ao concluir o puzzle")]
    public GameObject caixa;            // Caixa que será aberta ao concluir o puzzle (opcional)

    [Tooltip("Tampa da caixa que desaparece quando o puzzle é concluído")]
    public GameObject tampaCaixa;       // A tampa que vai sumir quando todas as pinturas forem encaixadas

    // Chamado pelos Slots quando uma peça correta é encaixada
    public void ContarAcerto()
    {
        acertos++;

        Debug.Log(" Peça correta encaixada! Total: " + acertos + "/" + totalSlots);

        if (acertos >= totalSlots)
        {
            Debug.Log(" Quebra-cabeça resolvido!");

            // Acende a luz
            //if (rewardLight != null)
                rewardLight.enabled = true;

            // Ativa a caixa
            if (caixa != null)
                caixa.SetActive(true);

            // Faz a tampa desaparecer
            if (tampaCaixa != null)
            {
                tampaCaixa.SetActive(false);
                Debug.Log(" Tampa da caixa removida da cena!");
            }
            else
            {
                Debug.LogWarning(" Nenhuma tampa foi atribuída no PuzzleManager!");
            }
        }
    }
}