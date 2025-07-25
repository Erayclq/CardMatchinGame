using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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
        CreateCards();
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
        // Grid Layout ayarı
        GridLayoutGroup grid = gridTransform.GetComponent<GridLayoutGroup>();
        if (grid != null)
        {
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = GameData.columns;
        }

        int totalCards = GameData.rows * GameData.columns;
        spritePairs = new List<Sprite>();

        for (int i = 0; i < totalCards / 2; i++)
        {
            Sprite sprite = Sprites[i % Sprites.Length]; // fazla sprite yoksa döngüye al
            spritePairs.Add(sprite);
            spritePairs.Add(sprite);
        }

        ShuffleSprites(spritePairs);

        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetIconSprite(spritePairs[i]);
            card.controller = this;
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
                a.MakeInvisible();
            });

            b.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                b.MakeInvisible();
            });
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

        a.MakeVisible();
        b.MakeVisible();

        a.Hide(); // Ters Döndürmek için.
        b.Hide();

        a.transform.DOScale(Vector3.one, 0.2f);
        b.transform.DOScale(Vector3.one, 0.2f);

        matchCount--;
    }

}
