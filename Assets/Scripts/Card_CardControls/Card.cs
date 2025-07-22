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
        // Example rotation animation using DOTween (rotates to 0, 180, 0 over 0.5 seconds)
        transform.DORotate(new Vector3(0, 180, 0), delay).OnComplete(()=>
        {
            iconImage.sprite = iconSprite;
        });
            //ShowDelay(); // bunun daha kolay bir yolu var mı ??
        isSelected = true;
    }

    /*private IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(0.1f);
        iconImage.sprite = iconSprite;
    }*/

    public void Hide()
    {
        transform.DORotate(new Vector3(0, 0, 0), delay).OnComplete(()=>
        {
            iconImage.sprite = hiddenSpriteImage;
        });;
            //HideDelay(); // bunun daha kolay bir yolu var mı ??
        isSelected = false;
    }
    /*private IEnumerator HideDelay()
    {
        yield return new WaitForSeconds(0.1f);
        iconImage.sprite = hiddenSpriteImage;
    }*/
}
