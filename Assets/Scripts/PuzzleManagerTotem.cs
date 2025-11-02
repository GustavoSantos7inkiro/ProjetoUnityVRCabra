using System.Collections;
using UnityEngine;

public class PuzzleManagerTotem : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    public int totalSlots = 3;            // Número de totens
    private int acertos = 0;              // Contador de totens encaixados

    [Header("Luzes do ambiente")]
    public Light[] lightsToFade;          // Arraste aqui as luzes que devem apagar
    public float fadeDuration = 2f;       // Duração do fade em segundos

    [Header("Cabra e Canvas Fim")]
    public GameObject cabra;              // Arraste a cabra
    public GameObject canvasFim;          // Canvas do "FIM" em world space

    public void ContarAcerto()
    {
        acertos++;
        Debug.Log($"Totens encaixados: {acertos}/{totalSlots}");

        if (acertos >= totalSlots)
        {
            StartCoroutine(ApagarLuzesEMostrarFim());
        }
    }

    private IEnumerator ApagarLuzesEMostrarFim()
    {
        float timer = 0f;
        Light[] allLights = lightsToFade;

        // Salva intensidade original
        float[] originalIntensities = new float[allLights.Length];
        for (int i = 0; i < allLights.Length; i++)
            originalIntensities[i] = allLights[i].intensity;

        // Fade out
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            for (int i = 0; i < allLights.Length; i++)
            {
                if (allLights[i] != null)
                    allLights[i].intensity = Mathf.Lerp(originalIntensities[i], 0f, timer / fadeDuration);
            }
            yield return null;
        }

        // Garante intensidade zero
        for (int i = 0; i < allLights.Length; i++)
        {
            if (allLights[i] != null)
                allLights[i].intensity = 0f;
        }

        // Desativa a cabra
        if (cabra != null)
            cabra.SetActive(false);

        // Ativa o Canvas FIM
        if (canvasFim != null)
            canvasFim.SetActive(true);
    }
}
