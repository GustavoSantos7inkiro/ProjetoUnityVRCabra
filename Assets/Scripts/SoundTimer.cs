using UnityEngine;

public class SoundTimer : MonoBehaviour
{
     public AudioSource audioSource;  // Arraste aqui o AudioSource
    public float interval = 20f;     // Tempo em segundos (1 minuto)
    private float timer;

    void Start()
    {
        timer = interval; // começa a contar desde o início
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            audioSource.Play(); // toca o som
            timer = interval;   // reinicia o contador
        }
    }
}
