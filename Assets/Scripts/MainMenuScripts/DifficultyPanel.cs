using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] RectTransform hardButton;
    [SerializeField] RectTransform mediumButton;
    [SerializeField] RectTransform eEasyButton;

    [Header("Audio Clip")]
    [Tooltip("Tuslara tiklandiginda calinacak audioclip")]
    [SerializeField] AudioClip audioClip;

    public void SelectHard()
    {
        DifficultyManager(30f, 3, 6);
    }

    public void SelectMedium()
    {
        DifficultyManager(60f, 3, 4);
    }

    public void SelectEasy()
    {
        DifficultyManager(90f, 2, 3);
    }

    public void DifficultyManager(float seconds, int rows, int columns)
    {
        SoundFXManager.instance.PlaySoundFXClip(audioClip, transform, 1f);
        GameData.selectedSecond = seconds;
        GameData.rows = rows;
        GameData.columns = columns;
        SceneManager.LoadScene("GameScene");
    }

}
