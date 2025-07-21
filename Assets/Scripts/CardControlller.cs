using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardControlller : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] Sprite[] Sprites;
    private List<Sprite> spritePairs;

    Card firstSelected;
    Card secondSelected;

    void Start()
    {
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
            a.gameObject.SetActive(false); 
            b.gameObject.SetActive(false);
        }
        else // eğer eşleşmiyorlarsa geri döndür.
        {
            a.Hide(); 
            b.Hide();
        }
    }

}
