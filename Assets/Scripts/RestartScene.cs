using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public string menuSceneName = "Menu"; // Coloque exatamente o nome da sua cena do menu

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }
}