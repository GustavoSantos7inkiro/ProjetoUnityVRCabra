using UnityEngine;
using System.Collections;

public class PuzzleManagerTotem : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    [Tooltip("Quantos totens precisam ser encaixados nos slots.")]
    public int totalSlots = 3;
    [HideInInspector] public int acertos = 0;

    [Header("Referência dos Slots")]
    [Tooltip("Arraste aqui todos os slots de totem da cena.")]
    public SlotTotem[] slotsTotem;

    [Header("Luzes que serão apagadas")]
    [Tooltip("Arraste aqui as luzes que devem apagar com fade quando o puzzle for concluído.")]
    public Light[] luzesParaApagar;

    [Header("Configuração do Fade")]
    [Tooltip("Tempo em segundos para o fade out das luzes.")]
    public float tempoFade = 2.5f;

    private bool puzzleResolvido = false;

    private void Start()
    {
        if (slotsTotem == null || slotsTotem.Length == 0)
            slotsTotem = FindObjectsOfType<SlotTotem>();
    }

    // Chamado pelos slots quando um totem é corretamente encaixado
    public void ContarAcerto()
    {
        acertos++;
        Debug.Log("Totem encaixado! Total: " + acertos + "/" + totalSlots);

        if (!puzzleResolvido && acertos >= totalSlots)
        {
            puzzleResolvido = true;
            Debug.Log("✅ Puzzle dos Totens resolvido!");
            StartCoroutine(ApagarLuzes());
        }
    }

    // Coroutine para apagar as luzes com fade out suave
    private IEnumerator ApagarLuzes()
    {
        foreach (Light luz in luzesParaApagar)
        {
            if (luz == null) continue;

            float intensidadeInicial = luz.intensity;
            float t = 0f;

            while (t < tempoFade)
            {
                t += Time.deltaTime;
                luz.intensity = Mathf.Lerp(intensidadeInicial, 0f, t / tempoFade);
                yield return null;
            }

            luz.intensity = 0f;
            luz.enabled = false;
        }

        Debug.Log("💡 Todas as luzes foram apagadas com sucesso!");
    }
}