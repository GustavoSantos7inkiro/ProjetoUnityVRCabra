using UnityEngine;

public class CabraJumpscare : MonoBehaviour
{
    public AudioClip gritoCabra;  // Arraste aqui o áudio do grito
    private AudioSource audioSource;

    void Awake()
    {
        // Adiciona o AudioSource dinamicamente
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gritoCabra;
        audioSource.loop = true;       // Fica em loop
        audioSource.playOnAwake = false;
    }

    public void AtivarJumpscare()
    {
        // Toca o som
        audioSource.Play();

        // Garante que a cabra esteja ativa
        gameObject.SetActive(true);
    }

    public void PararJumpscare()
    {
        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
