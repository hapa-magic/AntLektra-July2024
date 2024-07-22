using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HapaMagic
{
    public class HandManager : MonoBehaviour
    {
        public GameObject cardPrefab; // Assign in inspector
        public Transform handTransform; // Root of hand position
        public List<GameObject> cardsInHand = new List<GameObject>();
        public List<Transform> cardPositions;
        public int maxHandSize = 5;
        void Start()
        {
            
        }

        public void AddCardToHand(Card cardData)
        {
            if (cardsInHand.Count < maxHandSize)
            {
                GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
                cardsInHand.Add(newCard);

                newCard.GetComponent<CardDisplay>().cardData = cardData;

                UpdateHandVisuals();
            } else
            {
                Debug.Log("Too many cards!");
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

    }
}
