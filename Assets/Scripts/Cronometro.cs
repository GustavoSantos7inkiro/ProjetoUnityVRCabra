using UnityEngine;
using TMPro;
using System.Collections;

public class CronometroVR : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerHead;        // Main Camera (filha do XR Origin)
    public Canvas canvas;               // Canvas que contém o TMP Text
    public TextMeshProUGUI tempoText;   // Texto do cronômetro
    public Light luz1;
    public Light luz2;
    public GameObject cabraJumpscarePrefab;

    [Header("Offset do cronômetro")]
    public Vector3 cronometroOffset = new Vector3(0.7f, -0.07f, 0.5f); // posição fixa do cronômetro no VR

    [Header("Offset da cabra")]
    public Vector3 cabraOffset = new Vector3(0f, -0.07f, 0.5f); // posição da cabra na frente do jogador

    [Header("Fade das luzes")]
    public float fadeDuration = 10f; // 10 segundos

    [Header("Cronômetro")]
    public float tempoSegundos = 15 * 60f; // 15 minutos
    private bool rodando = true;

    void Update()
    {
        if (!rodando) return;

        // Contagem regressiva
        tempoSegundos -= Time.deltaTime;
        if (tempoSegundos < 0f) tempoSegundos = 0f;

        int minutos = Mathf.FloorToInt(tempoSegundos / 60f);
        int segundos = Mathf.FloorToInt(tempoSegundos % 60f);
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Mantém o canvas na frente do jogador
        if (playerHead != null && canvas != null)
        {
            canvas.transform.position = playerHead.position + playerHead.TransformDirection(cronometroOffset);
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - playerHead.position);
        }

        // Quando chega a 00:00
        if (tempoSegundos <= 0f)
        {
            rodando = false;
            StartCoroutine(ApagarLuzesEJumpscare());
        }
    }

    IEnumerator ApagarLuzesEJumpscare()
    {
        float t = 0f;
        float intensidadeInicial1 = luz1.intensity;
        float intensidadeInicial2 = luz2.intensity;

        // Fade out das luzes
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float fator = 1f - t / fadeDuration;
            if (luz1 != null) luz1.intensity = intensidadeInicial1 * fator;
            if (luz2 != null) luz2.intensity = intensidadeInicial2 * fator;
            yield return null;
        }

        if (luz1 != null) luz1.enabled = false;
        if (luz2 != null) luz2.enabled = false;

        // Instancia a cabra na frente do jogador
        if (cabraJumpscarePrefab != null && playerHead != null)
        {
            Vector3 posJumpscare = playerHead.position + playerHead.TransformDirection(cabraOffset);
            GameObject cabra = Instantiate(cabraJumpscarePrefab, posJumpscare, Quaternion.LookRotation(playerHead.forward));
            cabra.transform.SetParent(playerHead, true); // gruda na cabeça do jogador
        }
    }

    public void PararCronometro()
    {
        rodando = false;
    }

    public void ReiniciarCronometro()
    {
        tempoSegundos = 15 * 60f;
        rodando = true;
    }
}
