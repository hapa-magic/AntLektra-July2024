using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using HapaMagic;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image imageData;
    public Image AntCountImage;
    public TMP_Text costText;
    public TMP_Text nameText;
    public TMP_Text statsNum;
    public TMP_Text numAnts;
    public TMP_Text effect1;
    public TMP_Text effect2;
    public TMP_Text effect3;
    void Start()
    {
        UpdateCardDisplay();
    }

    

    public void UpdateCardDisplay() {
        imageData.sprite = cardData.cardSprite;
        nameText.text = cardData.cardName;
        if (cardData.health != 0)
        {
            statsNum.text = cardData.attack.ToString() + '/' + cardData.health.ToString();
        }
        costText.text = cardData.eggCost.ToString();
        cardData.SetNumAnts(new System.Random());
        if (cardData.numAnts > 0)
        {
            numAnts.text = cardData.numAnts.ToString();
        } else
        {
            AntCountImage.color = Color.clear;
            numAnts.text = "";
        }
        effect1.text = cardData.effect[0].description;
        if (cardData.effect[1].effectAbility != Effect.EffectAbility.None)
        {
            effect2.text = cardData.effect[1].description;
        }
        if (cardData.effect[2].effectAbility != Effect.EffectAbility.None)
        {
            effect2.text = cardData.effect[2].description;
        }
    }
    public void UpdateCardDisplay(int newNumAnts) {
        nameText.text = cardData.cardName;
        if (cardData.health != 0)
        {
            statsNum.text = cardData.attack.ToString() + '/' + cardData.health.ToString();
        }
        costText.text = cardData.eggCost.ToString();
        cardData.SetNumAnts(newNumAnts);
        if (newNumAnts > 0)
        {
            numAnts.text = newNumAnts.ToString();
        } else
        {
            AntCountImage.color = Color.clear;
            numAnts.text = "";
        }
        effect1.text = cardData.effect[0].description;
        if (cardData.effect[1].effectAbility != Effect.EffectAbility.None)
        {
            effect2.text = cardData.effect[1].description;
        }
        if (cardData.effect[2].effectAbility != Effect.EffectAbility.None)
        {
            effect2.text = cardData.effect[2].description;
        }
    }
}

