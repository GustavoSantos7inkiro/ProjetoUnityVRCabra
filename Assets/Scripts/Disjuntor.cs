using UnityEngine;

public class Disjuntor : MonoBehaviour
{
    [Header("Configurações dos Fusíveis")]
    public int fusiveisNecessarios = 3;
    private int fusiveisInseridos = 0;

    [Header("Lâmpada que acende quando completo")]
    public Light lampada;

    [Header("Números que aparecem quando a luz acende")]
    public GameObject[] numeros; // arraste aqui os objetos dos números no Inspector

    private void Start()
    {
        lampada.enabled = false; // começa apagada
        AtualizarVisibilidadeNumeros(false); // números invisíveis no início
    }

    public void ContarFusivel()
    {
        fusiveisInseridos++;

        if (fusiveisInseridos >= fusiveisNecessarios)
        {
            lampada.enabled = true;
            AtualizarVisibilidadeNumeros(true);
            Debug.Log("Todos os fusíveis foram inseridos! Lâmpada acesa e números revelados!");
        }
    }

    private void AtualizarVisibilidadeNumeros(bool visivel)
    {
        foreach (GameObject numero in numeros)
        {
            if (numero != null)
                numero.SetActive(visivel);
        }
    }
}