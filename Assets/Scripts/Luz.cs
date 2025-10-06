using UnityEngine;

public class LuzEspecial : MonoBehaviour
{
    public Material materialNormal;  // preto padrão
    public Material materialEspecial; // textura alternativa

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = materialNormal; // começa com preto
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LuzMagica"))
        {
            rend.material = materialEspecial; // troca a textura
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LuzMagica"))
        {
            rend.material = materialNormal; // volta ao normal
        }
    }
}
