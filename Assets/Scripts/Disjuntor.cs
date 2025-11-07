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

    [Header("Som do Disjuntor (final)")]
    public AudioSource somDisjuntor;

    [Header("Sons de Click dos Fusíveis (variações)")]
    public AudioClip[] sonsClick;     // coloque várias variações do click aqui
    public AudioSource audioClicks;   // outro AudioSource no disjuntor
    private int indiceClick = 0;      // controla qual som tocar

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
        // 🔊 TOCAR CLICK DO FUSÍVEL
        TocarSomClick();

        fusiveisInseridos++;

        if (fusiveisInseridos >= fusiveisNecessarios)
        {
            AcenderLampada();
        }
    }

    private void TocarSomClick()
    {
        if (sonsClick.Length == 0 || audioClicks == null)
            return;

        audioClicks.clip = sonsClick[indiceClick];
        audioClicks.volume = 0.6f; // volume médio
        audioClicks.Play();

        // avança para o próximo som
        indiceClick++;

        // se passou do tamanho, volta para 0
        if (indiceClick >= sonsClick.Length)
            indiceClick = 0;
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

        // ✅ Som final
        if (somDisjuntor != null)
        {
            somDisjuntor.volume = 1.2f;
            somDisjuntor.Play();
        }

        if (OnLampadaAcende != null)
            OnLampadaAcende.Invoke();

        Debug.Log("Todos os fusíveis foram inseridos!");
    }
}