using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HapaMagic {
        
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")] 
    public class Card : ScriptableObject {
        public string cardName;
        public int eggCost;
        public float timeActive;
        public int minAnts;
        public int maxAnts;
        public int numAnts { set; get; }
        public List<Effect> effect = new List<Effect>(3);
        public GameObject ant;
        public Sprite cardSprite;
        public DeckType deckType;
        public int health;
        public int attack;
        private void Awake()
        {
            SetNumAnts(new System.Random());
        }
        public enum DeckType
        {
            Base,
            Beetle,
            Mantis,
        }

        public void SetNumAnts(System.Random rnd)
        {
            numAnts = rnd.Next(minAnts, maxAnts + 1);
        }
        public void SetNumAnts(int numAnts)
        {
            this.numAnts = numAnts;
        }
    }
}