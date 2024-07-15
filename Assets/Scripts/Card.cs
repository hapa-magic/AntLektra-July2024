using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Database;

namespace HapaMagic {
    public struct Effect {
            public string effectName;
            public string description;
            public string effectType;
            public Effect(string effectName, string description, string effectType) {
                this.effectName = effectName;
                this.description = description;
                this.effectType = effectType;
            }
            public void SetActive() {

            }
        };
        
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")] 
    public class Card : ScriptableObject {
        public string cardName;
        public int eggCost;
        public float timeActive;
        public int antNum;
        public List<Effect> effect;
        public GameObject ant;
        public SpriteRenderer cardFace;
        public int minHealth;
        public int maxHealth;
        public int minAttack;        
        public int maxAttack;
        
        public enum DeckType{
            Base,
            Beetle,
            Mantis,
        }

        public enum EffectType {
            Instant,
            Active,
            Power
        }

        public enum Effect{
            None,
            SpawnBasicAnt,
            SpawnHoneyAnt,
            SpawnBeetleAnt,
            SpawnMantisAnt,
            Cycle,
            Draw,
            Discard,
        }

        public struct CardEffect{
            public EffectType type;
            public Effect effect;
        }


        // Card(int eggCost, int recharge, int antNum, string cardName, bool hasAnt,
        //     AntController ant, int numEffects, Effect[] effect, 
        //     SpriteRenderer cardFace) {
        //     this.eggCost = eggCost;
        //     this.recharge = recharge;
        //     this.antNum = antNum;
        //     if (hasAnt) {
        //         this.ant = ant;
        //         this.health = ant.health;
        //         this.attack = ant.attack;
        //     } else {
        //         this.health = -1;
        //         this.attack = -1;
        //     }
        //     if (numEffects != 0) {
        //         for (int i = 0; i < numEffects; ++i) {
        //             this.effect[i] = effect[i];
        //         }
        //     }
        //     this.cardName = cardName;
        //     this.cardFace = cardFace;
        // }

        public void SetEggCost(int eggCost) {
            this.eggCost = eggCost;
        }
        public void ChangeEggCost(float percent) {
            percent *= eggCost;
            eggCost = (int)Math.Round(percent);
        }
        public void SetTimeActive(int timeActive) {
            this.timeActive = timeActive;
        }
        public void ChangeRecharge(float percent) {
            percent *= timeActive;
            timeActive = (int)Math.Round(percent);
        }
        public void SetAntNum(int antNum) {
            this.antNum = antNum;
        }
        public void ChangeAntNum(int antNum) {
            this.antNum += antNum;
        }
        public void SetAnt(GameObject ant) {
            this.ant = ant;
        }
        public void SetCardFace(SpriteRenderer cardFace) {
            this.cardFace = cardFace;
        }
    }
}