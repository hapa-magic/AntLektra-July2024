using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapaMagic;

public class CardPreview : MonoBehaviour
{
    Canvas canvas;
    GameObject previewCard;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void PreviewCard(GameObject card) {
        previewCard = Instantiate(card, canvas.gameObject.transform.position, Quaternion.identity, canvas.transform);
        previewCard.transform.localScale *= 5;
    }
    public void DestroyPreview(){
        Destroy(previewCard);
    }
}
