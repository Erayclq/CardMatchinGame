using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverPanelButtons : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField] private RectTransform Timer;
    [SerializeField] private GameObject Clock;
    [SerializeField] private GameObject PauseButton;
    void Start()
    {
        PauseButton.gameObject.SetActive(false);
        Clock.gameObject.SetActive(false);
        Timer.DOAnchorPos(new Vector2(81,-530), 1f); // Timer'ı yourscore'a getiren komut.
    }
    public void OnRetryClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
