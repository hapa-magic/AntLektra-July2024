using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapaMagic;
using TMPro;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    Transform parent;
    public float previewScaler;
    public TMP_Text attackText;
    public TMP_Text defenseText;
    public TMP_Text effectText;
    public Image previewImage;
    // Start is called before the first frame update
    void Start()
    {
        previewImage.enabled = false;
        parent = this.transform;
    }

    public void PreviewCard(Card cardData) {
        attackText.text = cardData.attack.ToString();
        defenseText.text = cardData.health.ToString();
        effectText.text = cardData.effect[0].description;
        previewImage.sprite = cardData.cardSprite;
        previewImage.enabled = true;
    }

    public void PreviewUnit(AntController ant)
    {

    }
    public void DestroyPreview(){
        attackText.text = "";
        defenseText.text = "";
        effectText.text = "";
        previewImage.enabled = false;
    }
}
