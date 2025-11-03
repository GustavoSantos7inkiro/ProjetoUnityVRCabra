using UnityEngine;

public class SlotManagerFusivel : MonoBehaviour
{
    [Header("Fusíveis")]
    public GameObject[] fusiveis; // Arraste todos os fusíveis aqui

    [Header("Posições dos slots (World Transform)")]
    public Vector3[] posicoes;

    [Header("Rotações dos slots (Euler X,Y,Z)")]
    public Vector3[] eulersRotacoes;

    [HideInInspector]
    public Disjuntor disjuntor;

    private bool[] slotOcupado; // Controle de slots preenchidos

    private void Awake()
    {
        disjuntor = GetComponentInParent<Disjuntor>();
        slotOcupado = new bool[posicoes.Length];
    }

    // Encaixa o fusível no próximo slot livre
    public void EncaixarFusivel(GameObject fusivel)
    {
        if (fusivel == null) return;

        Fusivel f = fusivel.GetComponent<Fusivel>();
        if (f == null || f.colocado) return;

        // Encontra o próximo slot livre
        int indexLivre = -1;
        for (int i = 0; i < slotOcupado.Length; i++)
        {
            if (!slotOcupado[i])
            {
                indexLivre = i;
                break;
            }
        }

        if (indexLivre == -1)
        {
            Debug.Log("Todos os slots já estão ocupados!");
            return;
        }

        // Move fusível para a posição e rotação do slot
        fusivel.transform.position = posicoes[indexLivre];
        fusivel.transform.rotation = Quaternion.Euler(eulersRotacoes[indexLivre]);

        // Marca como colocado e slot ocupado
        f.colocado = true;
        slotOcupado[indexLivre] = true;

        // Desativa física
        Rigidbody rb = fusivel.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Conta no disjuntor
        if (disjuntor != null)
            disjuntor.ContarFusivel();

        Debug.Log($"Fusível {fusivel.name} encaixado no slot {indexLivre}!");
    }
}