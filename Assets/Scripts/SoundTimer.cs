using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    [Header("Referências de Áudio")]
    public AudioSource audioSource;    // Arraste o AudioSource da cabra
    public AudioClip berroSimples;     // Som do berro simples
    public AudioClip berroGrande;      // Som do berro grande

    [Header("Configuração de Tempo")]
    public float intervalo = 60f;      // Intervalo em segundos (1 minuto por padrão)

    private float timer;
    private bool proximoEhSimples = true; // alterna entre os tipos

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        timer = intervalo; // começa contando desde o início
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (audioSource != null)
            {
                // Alterna o som
                if (proximoEhSimples && berroSimples != null)
                {
                    audioSource.PlayOneShot(berroSimples);
                }
                else if (!proximoEhSimples && berroGrande != null)
                {
                    audioSource.PlayOneShot(berroGrande);
                }

                // Inverte o tipo de som para o próximo ciclo
                proximoEhSimples = !proximoEhSimples;
            }

            // Reinicia o temporizador
            timer = intervalo;
        }
    }
}
