using UnityEngine;
using UnityEngine.XR;

public class FootstepLoop : MonoBehaviour
{
    public AudioSource audioSource;     // arraste o som aqui
    public float movimentoMinimo = 0.05f; // velocidade mínima para considerar "andando"

    private Vector3 ultimaPosicao;

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }

        ultimaPosicao = transform.parent.position;
    }

 private void Update()
{
#if UNITY_EDITOR
    // ===== MODO PC (Device Simulator) =====
    bool estaAndando = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

    if (estaAndando)
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
    else
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
#else
    // ===== MODO VR REAL =====
    Vector3 posicaoAtual = transform.parent.position;
    float velocidade = (posicaoAtual - ultimaPosicao).magnitude / Time.deltaTime;
    ultimaPosicao = posicaoAtual;

    bool estaAndando = velocidade > movimentoMinimo;

    if (estaAndando)
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
    else
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
#endif
}
}
