using System.Collections;
using System.Collections.Generic;
using HapaMagic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHandSize = 5;

    private int currentIndex = 0;
    public int maxHandSize = 5;
    public EggTracker eggTracker;
    public int eggCost;
    public int currentHandSize;
    private HandManager handManager;
    public TMP_Text drawCostText;


    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        handManager = FindObjectOfType<HandManager>();
        maxHandSize = handManager.maxHandSize;
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            DrawCard(handManager);
        }
        eggCost = 10;
        UpdateEggCostNum();
        StartCoroutine(DecayEggCost());
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
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
        if (allCards.Count == 0)
            return;

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
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
