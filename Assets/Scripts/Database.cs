using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using HapaMagic;

public class Database : MonoBehaviour {
    public string effectsFile = "AntLektraCardEffects.csv";
    public static Database Instance { get; private set; }
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
    public List<Effect> effects = new List<Effect>();

    void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;

        using (StreamReader sr = File.OpenText(effectsFile)) {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                string[] words = s.Split(',');
                effects.Add(new Effect(words[0], words[1], words[2]));
            }
        }

        foreach (Effect effect in effects) {
            Debug.Log(effect.effectName);
        }
    }

  void Update() {
    // global game update logic goes here
  }
    public void SetEffect(string effectName, int power) {
        switch (effectName) {
            
            case "Draw":
                Draw(ref power);
                break;

            case "Discard":
                Debug.Log("Player discards a card");
                break;

            case "Increase Eggs/min":
                Debug.Log("Activated " + effectName);
                break;

            case "Cycle":
                Discard(ref power);
                Draw(ref power);
                break;
        }
        
    } 

    public void Draw(ref int power) {
        for (int i = 0; i < power; ++i) {
            Debug.Log("Player draws " + i + " card(s)");
        }
    }

    public void Discard(ref int power) {
        for (int i = 0; i < power; ++i) {
            Debug.Log("Player discards " + i + " cards(s).");
    }

}
}
