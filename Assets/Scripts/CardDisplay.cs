using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HapaMagic;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image imageData;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Effect effect1;
    public Effect effect2;
    public Effect effect3;
    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay() {
        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = cardData.attack.ToString();
    }
}

