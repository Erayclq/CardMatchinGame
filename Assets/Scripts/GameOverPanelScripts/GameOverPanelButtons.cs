using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelButtons : MonoBehaviour
{
    public void OnRetryClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
