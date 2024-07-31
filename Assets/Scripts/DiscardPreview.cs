using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HapaMagic;
using UnityEngine.UI;

public class DiscardPreview : MonoBehaviour
{
    [SerializeField] private Transform card;
    private Transform selectedCard;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject cancelButton;
    private HandManager handManager;

    void Awake() {
        handManager = GameObject.Find("HandManager").GetComponent<HandManager>();
    }

    public void PreviewDiscard(Transform newCard) {
        card = Instantiate(newCard, transform);
        selectedCard = newCard;
        card.GetComponent<CardMovement>().enabled = false;
        ActivateButtons();
    }

    public void DestoryPreview() {
        if (card != null) {
            Destroy(card);
            // card.gameObject.SetActive(false);
        }
        if (selectedCard != null) {
            selectedCard = null;
        }
        DeactivateButtons();
    }

    private void ActivateButtons() {
        confirmButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    private void DeactivateButtons() {
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
    }
    public void ConfirmDiscard() {
        handManager.cardsInHand.Remove(selectedCard.gameObject);
    }

    public void CancelDiscard() {

    }
}

