using System.Collections;
using System.Collections.Generic;
using HapaMagic;
using TMPro;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> startingDeck = new List<Card>();
    public List<int> startingCardsCountPer = new List<int>();

    public int startingHandSize = 5;

    private int currentIndex = 0;
    public int maxHandSize = 5;
    public int currentHandSize;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    public TMP_Text drawCostText;
    private bool startBattleRun = true;

    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        // allCards.AddRange(cards);
        GenerateStartingDeck(cards);

        handManager = FindObjectOfType<HandManager>();
        maxHandSize = handManager.maxHandSize;
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            drawPileManager.DrawCard(handManager);
        }
    }
    private void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindObjectOfType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindObjectOfType<HandManager>();
        }
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }

    public bool DrawCard() {
        if (drawPileManager == null) return false;
        return drawPileManager.DrawCard(handManager);
    }

    public bool CanDraw()
    {
        return currentHandSize < maxHandSize;
    }
    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        startBattleRun = false;
    }

    private void GenerateStartingDeck(Card[] resourceCards)
    {
        for (int i = 0; i < resourceCards.Length; i++)
        {
            switch (resourceCards[i].cardName)
            {
                case "Basic Ant":
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    break;

                case "Honey Ant":
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    break;

                case "Harvest Eggs":
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    break;

                case "Soldier Ant":
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    allCards.Add(resourceCards[i]);
                    break;
            }
            Utility.Shuffle(allCards);
        }
    }
}
