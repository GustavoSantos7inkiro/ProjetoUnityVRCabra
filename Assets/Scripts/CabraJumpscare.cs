using UnityEngine;

public class CabraJumpscare : MonoBehaviour
{
    public Transform playerHead;        // Para posicionar a cabra em frente ao jogador
    public Vector3 offset = new Vector3(0f, -0.07f, 0.5f); // Ajuste da posição
    public AudioClip gritoCabra;       // Áudio do grito da cabra
    private AudioSource audioSource;

    void Awake()
    {
        // Cria AudioSource se não existir
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

   public void AtivarJumpscare()
{
    if (playerHead == null)
    {
        Debug.LogWarning("PlayerHead não atribuído no CabraJumpscare!");
        return;
    }

    // Posiciona a cabra na frente do jogador
    transform.position = playerHead.position + playerHead.TransformDirection(offset);
    transform.rotation = Quaternion.LookRotation(playerHead.forward);
    transform.SetParent(playerHead);

    // Toca o grito em loop
    if (gritoCabra != null)
    {
        audioSource.clip = gritoCabra;
        audioSource.loop = true;       // ativa loop
        audioSource.Play();
    }
    else
    {
        Debug.LogWarning("GritoCabra não atribuído no prefab!");
    }
}
    public void PararJumpscare()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
