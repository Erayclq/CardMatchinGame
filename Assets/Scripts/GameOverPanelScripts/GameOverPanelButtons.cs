using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverPanelButtons : MonoBehaviour
{
    [Header("Scoreboard")]
    [SerializeField] private RectTransform Timer;

    void Start()
    {
        Timer.DOAnchorPos(new Vector2(81, 0), 1f); // Timer'ı yourscore'a getiren komut.
    }
    public void OnRetryClick()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
