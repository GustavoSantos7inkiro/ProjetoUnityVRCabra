using UnityEngine;

public class SlotManagerFusivel : MonoBehaviour
{
    [Header("Fusíveis e Slots")]
    public Vector3[] posicoes;       // Posições dos slots (world space)
    public Vector3[] rotacoesEuler;  // Rotação dos slots (Euler)

    [Header("Controle do Disjuntor")]
    public Disjuntor disjuntor;

    private bool[] slotOcupado;     // Controle de slots ocupados

    private void Awake()
    {
        if (posicoes.Length != rotacoesEuler.Length)
        {
            Debug.LogError("O tamanho dos arrays posicoes e rotacoesEuler deve ser igual!");
        }

        slotOcupado = new bool[posicoes.Length];
    }

    /// <summary>
    /// Encaixa um fusível no slot especificado.
    /// </summary>
    public void EncaixarFusivelNoSlot(Fusivel fusivel, int slotIndex)
    {
        if (fusivel == null || fusivel.colocado)
            return;

        if (slotIndex < 0 || slotIndex >= posicoes.Length)
        {
            Debug.LogWarning("SlotIndex fora do intervalo!");
            return;
        }

        if (slotOcupado[slotIndex])
            return;

        // Move fusível para posição e rotação do slot
        fusivel.TravarNoSlot(posicoes[slotIndex], rotacoesEuler[slotIndex]);

        slotOcupado[slotIndex] = true;

        if (disjuntor != null)
            disjuntor.ContarFusivel();
    }

    /// <summary>
    /// Opcional: desencaixa um fusível de um slot (para VR interativo)
    /// </summary>
    public void DesencaixarFusivel(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slotOcupado.Length)
            return;

        slotOcupado[slotIndex] = false;
    }
}
