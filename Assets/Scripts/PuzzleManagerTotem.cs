using UnityEngine;

public class PuzzleManagerTotem : MonoBehaviour
{
    [Header("Configuração do Puzzle de Totens")]
    [Tooltip("Quantidade total de slots de totens que precisam ser preenchidos.")]
    public int totalSlots = 3;

    private int acertos = 0;

    // Chamado pelos slots quando um totem é encaixado corretamente
    public void ContarAcerto()
    {
        acertos++;

        Debug.Log($"Totem correto encaixado! Total: {acertos}/{totalSlots}");

        // Verifica se todos os totens foram encaixados
        if (acertos >= totalSlots)
        {
            Debug.Log("🗿 Todos os totens foram posicionados corretamente!");
            // Aqui futuramente colocaremos:
            // - apagar luzes dos quartos
            // - tocar som, etc.
        }
    }
}
