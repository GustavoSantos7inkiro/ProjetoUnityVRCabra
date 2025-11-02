using UnityEngine;
using TMPro;
using System.Collections;

public class Cronometro : MonoBehaviour
{
    [Header("Referências")]
    public Transform playerHead;        

    [Header("Cronômetro")]
    public Canvas canvasCronometro;
    public TextMeshProUGUI tempoText;
    public Vector3 cronometroOffset = new Vector3(0.7f, -0.07f, 0.5f);
    public float tempoSegundos = 900f; // 15 minutos

    [Header("Luzes e Fade")]
    public Light luz1;
    public Light luz2;
    public float fadeDuration = 10f;

    [Header("Jumpscare Cabra")]
    public GameObject cabraJumpscarePrefab;
    public Vector3 cabraOffset = new Vector3(0f, -0.07f, 0.5f);

    [Header("Mensagem FIM")]
    public Canvas canvasFim;
    public TextMeshProUGUI fimText;
    public Vector3 fimOffset = new Vector3(-0.5f, 0.3f, 0.5f);

    private bool rodando = true;

    void Start()
    {
        if (canvasFim != null)
            canvasFim.gameObject.SetActive(false);
        if (fimText != null)
            fimText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!rodando) return;

        // Contagem regressiva
        tempoSegundos -= Time.deltaTime;
        if (tempoSegundos < 0f) tempoSegundos = 0f;

        // Atualiza o texto do cronômetro
        if (tempoText != null)
        {
            int minutos = Mathf.FloorToInt(tempoSegundos / 60f);
            int segundos = Mathf.FloorToInt(tempoSegundos % 60f);
            tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

        // Mantém o canvas do cronômetro na frente do jogador
        if (playerHead != null && canvasCronometro != null)
        {
            canvasCronometro.transform.position = playerHead.position + playerHead.TransformDirection(cronometroOffset);
            canvasCronometro.transform.rotation = Quaternion.LookRotation(canvasCronometro.transform.position - playerHead.position);
        }

        // Mantém o canvas FIM na frente do jogador
        if (playerHead != null && canvasFim != null)
        {
            canvasFim.transform.position = playerHead.position + playerHead.TransformDirection(fimOffset);
            canvasFim.transform.rotation = Quaternion.LookRotation(canvasFim.transform.position - playerHead.position);
        }

        // Chegou a 00:00
        if (tempoSegundos <= 0f)
        {
            rodando = false;
            StartCoroutine(ApagarLuzesEJumpscare());
        }
    }

    IEnumerator ApagarLuzesEJumpscare()
    {
        // Fade das luzes
        float t = 0f;
        float intensidadeInicial1 = luz1 != null ? luz1.intensity : 1f;
        float intensidadeInicial2 = luz2 != null ? luz2.intensity : 1f;

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
            cabra.transform.SetParent(playerHead, true);

            CabraJumpscare jumpscareScript = cabra.GetComponent<CabraJumpscare>();
            if (jumpscareScript != null)
            {
                jumpscareScript.playerHead = playerHead;
                if (jumpscareScript.gritoCabra != null)
                    jumpscareScript.AtivarJumpscare();
                else
                    Debug.LogWarning("Grito da cabra não atribuído no prefab!");
            }
        }

        // Ativa o Canvas e o texto FIM
        if (canvasFim != null) canvasFim.gameObject.SetActive(true);
        if (fimText != null) fimText.gameObject.SetActive(true);
    }

    public void PararCronometro()
    {
        rodando = false;
    }

    public void ReiniciarCronometro()
    {
        rodando = true;
    }
}