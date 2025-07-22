using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuButtons : MonoBehaviour
{ 
    [Header("Duration")]
    [SerializeField] float duration = 1f;

    [Header("UI Buttons")]
    public RectTransform playButton;
    public RectTransform settingsButton;
    public RectTransform quitButton;

    void Start()
    {
        playButton.DOScale(Vector3.zero, duration).From();
        settingsButton.DOScale(Vector3.zero, duration).From();
        quitButton.DOScale(Vector3.zero, duration).From();
    }
    public void OnPlayClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

}
