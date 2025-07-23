using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardControlller : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] Sprites;
    private List<Sprite> spritePairs;
    private Stack<Card> returnCardStack;

    [SerializeField] GameObject gameOverPanel;

    float matchCount = 0;

    Card firstSelected;
    Card secondSelected;

    [Header("Audio Clips")]
    [Tooltip("İki pair basariyla eslestirilrse calan Audio Clip.")]
    [SerializeField] private AudioClip succesfullySelected;

    [SerializeField] private Timer countdownTimer;


    void Start()
    {
        returnCardStack = new Stack<Card>();
        PrepareSprites();
        CreateCards();
    }

    public void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        for (int i = 0; i < Sprites.Length; i++)
        {
            spritePairs.Add(Sprites[i]);  // Her biri çift olarak bulunması gerektiği
            spritePairs.Add(Sprites[i]);  // için iki kere eklendi.
        }
        ShuffleSprites(spritePairs);
    }

    /// Fisher–Yates algoritmasıyla listeyi rastgele karıştırır.
    private void ShuffleSprites(List<Sprite> spritePairs)
    {
        for (int i = spritePairs.Count - 1; i > 0; i--)
        {
            // 0 ≤ j ≤ i arası rastgele bir indeks seç
            int j = Random.Range(0, i + 1);

            // swap list[i] ile list[j]
            Sprite temp = spritePairs[i];
            spritePairs[i] = spritePairs[j];
            spritePairs[j] = temp;
        }
    }

    public void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetIconSprite(spritePairs[i]);
            card.controller = this; // referans 
        }
    }

    public void SetSelected(Card card)
    {
        if (card.isSelected == false)
        {
            card.Show();
            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }
            else if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatch(firstSelected, secondSelected));
                firstSelected = null;
                secondSelected = null;
            }
        }
    }
    IEnumerator CheckMatch(Card a, Card b)
    {
        yield return new WaitForSeconds(0.5f);

        if (a.iconSprite == b.iconSprite) // Eğer eşleşme varsa gizle.
        {
            SoundFXManager.instance.PlaySoundFXClip(succesfullySelected, transform, 1f); // Eşleşme varsa succesfull audio clip calar.

            matchCount++; // eğer iki sprite aynı ise matchcountu arttır.
            if (matchCount == spritePairs.Count / 2) // hepsi eşleştiyse
            {
                gameOverPanel.SetActive(true); // game over paneli aktif eder eğer bütün pairlar eşleştiyse.
                                               // Ses animasyonu da eklenecek
                countdownTimer.StopTimer();
            }

            returnCardStack.Push(a);
            returnCardStack.Push(b);

            a.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                a.gameObject.SetActive(false);
            });

            b.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                b.gameObject.SetActive(false);
            });

            /*a.GetComponent<Image>().enabled = false;
            a.GetComponent<Card>().iconSprite = null;
            a.GetComponent<Button>().enabled = false;
            b.GetComponent<Image>().enabled = false;
            b.GetComponent<Card>().iconSprite = null;
            b.GetComponent<Button>().enabled = false;*/
        }
        else // eğer eşleşmiyorlarsa geri döndür tersini.
        {
            a.Hide();
            b.Hide();
        }
    }

    public void OnReturnClick()
    {
        Card a = returnCardStack.Pop();
        Card b = returnCardStack.Pop();

        a.gameObject.SetActive(true);
        b.gameObject.SetActive(true);

        a.Hide(); // Ters Döndürmek için.
        b.Hide();

        a.transform.DOScale(Vector3.one, 0.2f);
        b.transform.DOScale(Vector3.one, 0.2f);

        matchCount--;
    }

}
