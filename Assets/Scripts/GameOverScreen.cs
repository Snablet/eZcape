using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Wowy");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
