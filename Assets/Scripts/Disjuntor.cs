using UnityEngine;
using UnityEngine.Events;

public class Disjuntor : MonoBehaviour
{
    [Header("Configurações dos Fusíveis")]
    public int fusiveisNecessarios = 3;
    private int fusiveisInseridos = 0;

    [Header("Lâmpada que acende quando completo")]
    public Light lampada;

    [Header("Números que aparecem quando a luz acende")]
    public GameObject[] numeros; // arraste os objetos no Inspector

    [Header("Evento ao acender a lâmpada")]
    public UnityEvent OnLampadaAcende;

    private void Start()
    {
        if (lampada != null) lampada.enabled = false;
        AtualizarVisibilidadeNumeros(false);
    }

    // Chame este método quando um fusível for inserido
    public void ContarFusivel()
    {
        fusiveisInseridos++;

        if (fusiveisInseridos >= fusiveisNecessarios)
        {
            AcenderLampada();
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

    private void AcenderLampada()
    {
        if (lampada != null)
            lampada.enabled = true;

        AtualizarVisibilidadeNumeros(true);

        if (OnLampadaAcende != null)
            OnLampadaAcende.Invoke();

        Debug.Log("Todos os fusíveis foram inseridos! Lâmpada acesa e números revelados!");
    }
}