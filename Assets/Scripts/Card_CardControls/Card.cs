using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] Image iconImage;
    public Sprite hiddenSpriteImage;
    public Sprite iconSprite;

    [Tooltip("Kartlarin Donme Animasyonu Suresi")]
    [SerializeField] float delay = 0.2f;

    public bool isSelected;

    public CardControlller controller;

    [SerializeField] private AudioClip flipCardClip;

    public void OnCardClick()
    {
        SoundFXManager.instance.PlaySoundFXClip(flipCardClip, transform, 1f);
        controller.SetSelected(this); // card'ı parametre olarak ver.
    }

    public void SetIconSprite(Sprite sprite)
    {
        iconSprite = sprite;
    }
    public void Show()
    {
        transform.DORotate(new Vector3(0, 180, 0), delay).OnComplete(() =>
        {
            iconImage.sprite = iconSprite;
        });
        isSelected = true;
    }

    public void Hide()
    {
        transform.DORotate(new Vector3(0, 0, 0), delay).OnComplete(() =>
        {
            iconImage.sprite = hiddenSpriteImage;
        });
        isSelected = false;
    }

    public void MakeInvisible()
    {
        GetComponent<CanvasGroup>().alpha = 0f;         // Görünmez yap ama sahnede var olmaya devam eder.
        GetComponent<CanvasGroup>().interactable = false; // Tıklanamaz yap 
        GetComponent<CanvasGroup>().blocksRaycasts = false; // arkasındaki UI objesini etkileşime girilebilir yapar 
    }

    public void MakeVisible()
    {
        GetComponent<CanvasGroup>().alpha = 0f;         // Görünmez yap
        GetComponent<CanvasGroup>().interactable = false; // Tıklanamaz yap
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
