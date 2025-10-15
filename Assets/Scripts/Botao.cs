using UnityEngine;
using UnityEngine.SceneManagement;

public class Botao : MonoBehaviour
{
   public void MudarCena(string nomeCena)
   {
    SceneManager.LoadScene(nomeCena);
   }
}
