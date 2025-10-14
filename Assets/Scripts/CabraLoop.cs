using UnityEngine;
using System.Collections;

public class CabraLoop : MonoBehaviour
{
    [Header("Tempo Global (em minutos)")]
    [Range(3, 15)]
    public float globalTime = 5f; // valor inicial entre 3 e 15 minutos

    private float timeRemaining;
    private bool isRunning = false;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip[] audioClips; // um som pra cada etapa

    void Start()
    {
        StartCoroutine(GlobalTimerLoop());
    }

    IEnumerator GlobalTimerLoop()
    {
        isRunning = true;
        timeRemaining = globalTime * 60f; // converte minutos pra segundos

        // Cria os intervalos fixos em segundos
        float[] intervals = { 60f, 90f, 30f, 45f, 45f };

        int index = 0;
        float nextTrigger = intervals[index];

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Quando o tempo restante cruzar o ponto de ação
            if (globalTime * 60f - timeRemaining >= nextTrigger)
            {
                Debug.Log($"🔁 Loop {index + 1} executado em {nextTrigger} segundos.");
                // Aqui entra sua AÇÃO DO LOOP
                ExecuteLoopAction(index);

                // Passa pro próximo intervalo se existir
                index++;
                if (index < intervals.Length)
                    nextTrigger += intervals[index];
                else
                    break; // sai se não houver mais loops
            }

            yield return null;
        }

        // Espera 10 segundos após o fim do tempo
        Debug.Log("⏳ Tempo acabou. Esperando 10 segundos...");
        yield return new WaitForSeconds(10f);

        // Ação final
        FinalAction();

        isRunning = false;
    }

    void ExecuteLoopAction(int step)
    {
        // Aqui você coloca o que o loop deve fazer
        switch (step)
        {
            case 0:
                Debug.Log(" Som de batida na parede grande (1m).");
                break;
            case 1:
                Debug.Log(" Berro Grande (1m30s).");
                break;
            case 2:
                Debug.Log(" Batida simples (30s).");
                break;
            case 3:
                Debug.Log(" Berro simples (45s).");
                break;
            case 4:
                Debug.Log(" Passos(cascos) (45s).");
                break;
        }
    }

    void FinalAction()
    {
        Debug.Log("Momento junpScare!");
        // Coloca aqui o que deve acontecer no final (ex: terminar fase, spawnar algo, etc.)
    }
}

