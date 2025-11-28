using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    [Header("Referências de Áudio")]
    public AudioSource audioSource;      // Arraste o AudioSource da cabra
    public AudioClip berro1;             // Primeiro som
    public AudioClip berro2;             // Segundo som
    public AudioClip berro3; 
    public AudioClip berro4; 
    public AudioClip berro5;           // Terceiro som

    [Header("Configuração de Tempo")]
    public float intervalo = 60f;        // Intervalo entre sons

    private float timer;
    private int indiceSom = 0;           // Controla qual som será o próximo

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        timer = intervalo;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            TocarSomSequencial();
            timer = intervalo;
        }
    }

    private void TocarSomSequencial()
    {
        if (audioSource == null)
            return;

        // Alterna entre os três sons
        switch (indiceSom)
        {
            case 0:
                if (berro1 != null)
                    audioSource.PlayOneShot(berro1);
                break;

            case 1:
                if (berro2 != null)
                    audioSource.PlayOneShot(berro2);
                break;

            case 2:
                if (berro3 != null)
                    audioSource.PlayOneShot(berro3);
                break;

                case 3:
                if (berro4 != null)
                    audioSource.PlayOneShot(berro4);
                break;

                case 4:
                if (berro5 != null)
                    audioSource.PlayOneShot(berro5);
                break;
        }

        // Avança o índice para o próximo som
        indiceSom++;

        // Se passou de 2, volta para 0
        if (indiceSom > 2)
            indiceSom = 0;
    }
}
