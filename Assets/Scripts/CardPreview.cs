using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapaMagic;

public class CardPreview : MonoBehaviour
{
    Transform parent;
    public float previewScaler;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform;
    }

    public void PreviewCard(GameObject card) {
        GameObject previewCard = Instantiate(card, parent.position, 
                                             Quaternion.identity, parent);

        previewCard.name = "Preview Card";
        previewCard.GetComponent<CardMovement>().enabled = false;
        previewCard.GetComponentInChildren<Canvas>().sortingOrder = 1;
        previewCard.transform.localScale *= previewScaler;
        StartCoroutine(UpdateDisplayAfterDelay(previewCard, card.GetComponent<CardDisplay>().cardData.numAnts));
    }
    public void DestroyPreview(){
        GameObject gameObject = parent.Find("Preview Card").gameObject;
        Destroy(gameObject);
    }
    private IEnumerator UpdateDisplayAfterDelay(GameObject card, int numAnts) {
        yield return new WaitForSeconds(.2f);
        card.GetComponent<CardDisplay>().UpdateCardDisplay(numAnts);
    }
}
