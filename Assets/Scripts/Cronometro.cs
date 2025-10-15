using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerHead;        // Main Camera (filha do XR Origin)
    public Canvas canvas;               // Canvas que contém o TMP Text
    public TextMeshProUGUI tempoText;  // O texto que mostra o tempo

    [Header("Offset")]
    public Vector3 offset = new Vector3(0.3f, 0.25f, 0.5f); // X = direita, Y = acima, Z = à frente

    private float tempoSegundos = 0f;
    private bool rodando = true;

    void Update()
    {
        if (!rodando) return;

        // Atualiza o tempo
        tempoSegundos += Time.deltaTime;
        int minutos = Mathf.FloorToInt(tempoSegundos / 60f);
        int segundos = Mathf.FloorToInt(tempoSegundos % 60f);
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Mantém o canvas na frente do jogador
        if (playerHead != null && canvas != null)
        {
            canvas.transform.position = playerHead.position + playerHead.TransformDirection(offset);
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - playerHead.position);
        }
    }

    // Para o cronômetro
    public void PararCronometro()
    {
        rodando = false;
    }

    // Reinicia o cronômetro
    public void ReiniciarCronometro()
    {
        tempoSegundos = 0f;
        rodando = true;
    }
}