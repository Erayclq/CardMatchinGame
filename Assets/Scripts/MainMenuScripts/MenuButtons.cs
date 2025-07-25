using UnityEngine;
using DG.Tweening;

public class MenuButtons : MonoBehaviour
{
    [Header("Duration")]
    [SerializeField] float duration = 1f;

    [Header("UI Buttons")]
    public RectTransform playButton;
    public RectTransform settingsButton;
    public RectTransform quitButton;

    [Header("Audio Clip")]
    [Tooltip("Audio Clip plays when pressed the buttons")]
    [SerializeField] private AudioClip buttonClickClip;

    void Start()
    {
        playButton.DOScale(Vector3.zero, duration).From();
        settingsButton.DOScale(Vector3.zero, duration).From();
        quitButton.DOScale(Vector3.zero, duration).From();
    }

    public void OnPlayClick()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClickClip, transform, 1f);
    }

    public void OnQuitClick()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClickClip, transform, 1f);
        Application.Quit();
    }
    public void OnSettingsClick()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonClickClip, transform, 1f);
    }
}
