using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HapaMagic
{
    public class HandManager : MonoBehaviour
    {
        public GameObject cardPrefab; // Assign in inspector
        public Transform handTransform; // Root of hand position
        public GameObject discardPile;
        private DiscardManager discardManager;
        public List<GameObject> cardsInHand = new List<GameObject>();
        public List<Transform> cardPositions;
        public int maxHandSize = 5;
        void Start()
        {
            
        }

        public bool AddCardToHand(Card cardData)
        {
            if (cardsInHand.Count < maxHandSize)
            {
                GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
                cardsInHand.Add(newCard);

                newCard.GetComponent<CardDisplay>().cardData = cardData;

                UpdateHandVisuals();
                return true;
            } else
            {
                Debug.Log("Too many cards!");
                return false;
            }
        }

        private void UpdateHandVisuals()
        {
            int cardCount = cardsInHand.Count;

            for (int i = 0; i < cardCount; i++)
            {
                cardsInHand[i].transform.position = cardPositions[i].position;
            }
        }

        public void BattleSetup(int setMaxHandSize)
        {
            maxHandSize = setMaxHandSize;
        }

        public bool Discard() {
            if (cardsInHand.Count == 0) return false;
            if (cardsInHand.Count == 1) {
                discardManager.AddToDiscard(cardsInHand[0].GetComponent<Card>());
                cardsInHand.Clear();
                return true;
            } else {
                PromptForDiscard();
                return true;
            }
        }
        private void PromptForDiscard() {
            foreach (GameObject card in cardsInHand) {
                CardMovement cardMovement = card.GetComponent<CardMovement>();
                cardMovement.PromptForDiscard();
            }
        }
    }
}
