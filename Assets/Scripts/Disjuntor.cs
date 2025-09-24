using UnityEngine;

public class Disjuntor : MonoBehaviour
{
    public int fusiveisNecessarios = 3;
    private int fusiveisInseridos = 0;

    public Light lampada;

    private void Start()
    {
        lampada.enabled = false; // começa apagada
    }

    public void ContarFusivel()
    {
        fusiveisInseridos++;

        if (fusiveisInseridos >= fusiveisNecessarios)
        {
            lampada.enabled = true;
            Debug.Log("Todos os fusíveis foram inseridos! Lâmpada acesa!");
        }
    }
}
