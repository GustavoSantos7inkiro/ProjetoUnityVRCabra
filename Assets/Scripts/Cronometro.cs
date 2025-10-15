using UnityEngine;
using TMPro;
using System.Collections;

public class Cronometro : MonoBehaviour
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
    public float fadeDuration = 10f; // duração do fade em segundos

    [Header("Cronômetro")]
    public float tempoSegundos = 15 * 60f; // 15 minutos padrão, editável no Inspector
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
        float intensidadeInicial1 = luz1 != null ? luz1.intensity : 0f;
        float intensidadeInicial2 = luz2 != null ? luz2.intensity : 0f;

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
            GameObject cabraInstance = Instantiate(cabraJumpscarePrefab, Vector3.zero, Quaternion.identity);
            CabraJumpscare cabraScript = cabraInstance.GetComponent<CabraJumpscare>();

            // Posiciona a cabra
            cabraInstance.transform.position = playerHead.position + playerHead.TransformDirection(cabraOffset);
            cabraInstance.transform.rotation = Quaternion.LookRotation(playerHead.forward);
            cabraInstance.transform.SetParent(playerHead, true); // gruda na cabeça do jogador

            // Ativa o jumpscare (toca o som)
            if (cabraScript != null)
                cabraScript.AtivarJumpscare();
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
        rodando = true;
        // Mantém o valor definido no Inspector
        // tempoSegundos permanece o valor que você quiser para testes
    }
}