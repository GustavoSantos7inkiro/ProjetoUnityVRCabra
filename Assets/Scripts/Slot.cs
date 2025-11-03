using UnityEngine;

public class SlotManagerFusivel : MonoBehaviour
{
    public Vector3[] posicoes;
    public Vector3[] rotacoesEuler;

    private bool[] slotOcupado;
    public Disjuntor disjuntor;

    private void Awake()
    {
        slotOcupado = new bool[posicoes.Length];
        if (disjuntor == null)
            disjuntor = GetComponentInParent<Disjuntor>();
    }

    public void EncaixarFusivel(Fusivel fusivel)
    {
        if (fusivel == null || fusivel.colocado)
            return;

        int index = -1;
        for (int i = 0; i < slotOcupado.Length; i++)
            if (!slotOcupado[i])
            {
                index = i;
                break;
            }
        if (index == -1) return;

        fusivel.TravarNoSlot(posicoes[index], rotacoesEuler[index]);
        slotOcupado[index] = true;

        if (disjuntor != null)
            disjuntor.ContarFusivel();
    }
}