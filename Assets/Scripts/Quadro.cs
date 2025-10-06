using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int totalSlots = 3; // quantos slots precisam ser preenchidos
    private int acertos = 0;

    public void ContarAcerto()
    {
        acertos++;

        if (acertos >= totalSlots)
        {
            Debug.Log("Quebra-cabeça resolvido!");
            // aqui você pode abrir porta, ativar objeto, etc.
        }
    }
}