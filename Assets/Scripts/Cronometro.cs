using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerHead;        // Main Camera (filha do XR Origin)
    public Canvas canvas;               // Canvas que contém o TMP Text
    public TextMeshProUGUI tempoText;   // O texto que mostra o tempo

    [Header("Offset")]
    public Vector3 offset = new Vector3(0.7f, -0.07f, 0.5f); // Usa seus valores personalizados

    [Header("Tempo inicial (em segundos)")]
    public float tempoInicial = 900f; // 15 minutos = 900 segundos

    private float tempoRestante;
    private bool rodando = true;
    private bool luzesApagadas = false;

    void Start()
    {
        tempoRestante = tempoInicial;
    }

    void Update()
    {
        if (!rodando) return;

        // Conta regressivamente
        tempoRestante -= Time.deltaTime;
        if (tempoRestante <= 0f)
        {
            tempoRestante = 0f;
            rodando = false;
            if (!luzesApagadas)
            {
                ApagarLuzes();
                luzesApagadas = true;
            }
        }

        // Atualiza o texto
        int minutos = Mathf.FloorToInt(tempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tempoRestante % 60f);
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Mantém o cronômetro fixo na frente do jogador no VR
        if (playerHead != null && canvas != null)
        {
            canvas.transform.position = playerHead.position + playerHead.TransformDirection(offset);
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - playerHead.position);
        }
    }

    void ApagarLuzes()
    {
        // Encontra e apaga todas as luzes da cena
        Light[] luzes = FindObjectsOfType<Light>();
        foreach (Light luz in luzes)
        {
            luz.enabled = false;
        }

        Debug.Log("⏰ Tempo esgotado! Luzes apagadas!");
    }

    public void PararCronometro()
    {
        rodando = false;
    }

    public void ReiniciarCronometro()
    {
        tempoRestante = tempoInicial;
        rodando = true;
        luzesApagadas = false;
    }
}