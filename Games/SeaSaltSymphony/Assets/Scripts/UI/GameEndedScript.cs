using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndedScript : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
