using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] Image iconImage;

    public Sprite hiddenSpriteImage;
    public Sprite iconSprite;

    public bool isSelected;

    public CardControlller controller;
    

    public void OnCardClick()
    {
        controller.SetSelected(this); // card'ı parametre olarak ver.
    }

    public void SetIconSprite(Sprite sprite)
    {
        iconSprite = sprite;
    }
    public void Show()
    {
        // Example rotation animation using DOTween (rotates to 0, 180, 0 over 0.5 seconds)
        transform.DORotate(new Vector3(0, 180, 0), 0.2f)
            .OnComplete(() =>
            {
                iconImage.sprite = iconSprite;
            });
        isSelected = true;
    }
    public void Hide()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.2f).SetDelay(0.1f).OnComplete(() =>
        {
            iconImage.sprite = hiddenSpriteImage;
        });
        isSelected = false;
    }

}
