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
    public GameObject[] numeros;

    [Header("Som do Disjuntor")]
    [Tooltip("Coloque aqui o AudioSource com sfx_eletricidade_disjuntor_v1")]
    public AudioSource somDisjuntor;

    [Header("Evento ao acender a lâmpada")]
    public UnityEvent OnLampadaAcende;

    private void Start()
    {
        if (lampada != null) lampada.enabled = false;
        AtualizarVisibilidadeNumeros(false);
    }

    // Chamado quando um fusível é inserido
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

        // ✅ Tocar som do disjuntor
        if (somDisjuntor != null)
        {
            somDisjuntor.volume = 1.2f; // pode ajustar
            somDisjuntor.Play();
            Debug.Log("🔊 Som do disjuntor tocou!");
        }

        if (OnLampadaAcende != null)
            OnLampadaAcende.Invoke();

        Debug.Log("Todos os fusíveis foram inseridos! Lâmpada acesa e números revelados!");
    }
}