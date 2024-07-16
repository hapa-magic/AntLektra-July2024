using HapaMagic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;
    public HandManager handManager;
    public EggTracker eggTracker;
    public int eggCost;
    public TMP_Text drawCostText;
    // Start is called before the first frame update
    void Start()
    {
        // Load all card assets
        Card[] cards = Resources.LoadAll<Card>("Card Data");

        allCards.AddRange(cards);
        for (int i = 0; i < 5; i++)
        {
            DrawCard(handManager);
        }

        eggCost = 10;
        UpdateEggCostNum();
        StartCoroutine(DecayEggCost());
    }

    public void PayToDraw()
    {
        if (eggTracker.eggNum > eggCost)
        {
            DrawCard(handManager);
            eggTracker.eggNum -= eggCost;
            eggCost += 5;
            UpdateEggCostNum();
        }        
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0) return;

        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex+ 1) % allCards.Count;

    }
    public void UpdateEggCostNum()
    {
        drawCostText.text = "$ " + eggCost.ToString();
    }
    public IEnumerator DecayEggCost()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (eggCost > 10)
            {
                --eggCost;
                UpdateEggCostNum();
            }
        }
    }
}

