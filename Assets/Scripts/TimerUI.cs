using UnityEngine;
using UnityEngine.UI;
using TMPro; // se estiver usando TextMeshPro

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; // ou "Text" se não usar TMP
    public float timeRemaining = 60f; // tempo inicial em segundos
    public bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                OnTimerEnd();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void OnTimerEnd()
    {
        Debug.Log("Tempo acabou!");
        // aqui você pode chamar o loop ou mudar o estado do jogo
    }
}
