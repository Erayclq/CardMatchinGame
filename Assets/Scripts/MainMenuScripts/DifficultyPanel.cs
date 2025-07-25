using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] RectTransform hardButton;
    [SerializeField] RectTransform mediumButton;
    [SerializeField] RectTransform eEasyButton;

    public void SelectHard()
    {
        GameData.selectedSecond = 30f;
        GameData.rows = 3;
        GameData.columns = 6;
        SceneManager.LoadScene("GameScene");
    }

    public void SelectMedium()
    {
        GameData.selectedSecond = 60f;
        GameData.rows = 3;
        GameData.columns = 4;
        SceneManager.LoadScene("GameScene");
    }

    public void SelectEasy()
    {
        GameData.selectedSecond = 90f;
        GameData.rows = 2;
        GameData.columns = 3;
        SceneManager.LoadScene("GameScene");
    }
}
