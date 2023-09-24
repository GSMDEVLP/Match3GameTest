using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExittGame()
    {
        Application.Quit();
    }
}
