using UnityEngine;
using TMPro; // Para TextMeshPro

public class Cronometro : MonoBehaviour
{
    [Header("Referências de UI")]
    public TextMeshProUGUI textoCronometro; // Arraste o TextMeshPro do Canvas aqui

    [Header("Configurações")]
    public bool iniciarAutomaticamente = true;

    private float tempo = 0f;
    private bool rodando = false;

    void Start()
    {
        if (iniciarAutomaticamente)
            IniciarCronometro();
    }

    void Update()
    {
        if (!rodando) return;

        tempo += Time.deltaTime;
        AtualizarTexto();
    }

    public void IniciarCronometro()
    {
        rodando = true;
    }

    public void PararCronometro()
    {
        rodando = false;
    }

    public void ResetarCronometro()
    {
        tempo = 0f;
        AtualizarTexto();
    }

    private void AtualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tempo / 60f);
        int segundos = Mathf.FloorToInt(tempo % 60f);
        int centesimos = Mathf.FloorToInt((tempo * 100) % 100);
        textoCronometro.text = $"{minutos:00}:{segundos:00}:{centesimos:00}";
    }
}