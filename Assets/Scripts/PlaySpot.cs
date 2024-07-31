using System.Collections;
using System.Collections.Generic;
using HapaMagic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlaySpot : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    HomeNest homeNest;
    Card card;
    public DiscardManager discardManager;
    public HandManager handManager;
    public Image cardImage;

    void Start() 
    {
        homeNest = spawnObject.GetComponent<HomeNest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ActivateAbility(Effect effect, int power) 
    {
        if (homeNest != null) {
            switch (effect.effectType) {
                case Effect.EffectType.Active:
                    Debug.Log("Should be spawning ants soon");
                    switch (effect.effectAbility)
                    {
                        case Effect.EffectAbility.SpawnBasicAnt:
                            StartCoroutine(homeNest.SpawnAnt(homeNest._antPrefab, power));
                            break;

                        case Effect.EffectAbility.SpawnMantisAnt:
                            StartCoroutine(homeNest.SpawnAnt(homeNest._mantisAntPrefab, power));
                            break;

                        case Effect.EffectAbility.SpawnBeetleAnt:
                            StartCoroutine(homeNest.SpawnAnt(homeNest._beetleAntPrefab, power));
                            break;
                    }
                    break;

                case Effect.EffectType.Instant:
                    homeNest.PlayInstant(effect.effectAbility);
                        break;
            }
            return true;
        }
        return false;
    }
    public bool ActivateAbility(Card card, GameObject thisObj)
    {
        this.card = card;
        cardImage.sprite = card.cardSprite;
        cardImage.enabled = true;
        ActivateAbility(card.effect[0], card.numAnts);
        if (card.effect[1] != null)
        {
            ActivateAbility(card.effect[1], card.numAnts);
        }
        if (card.effect[2] != null)
        {
            ActivateAbility(card.effect[1], card.numAnts);
        }
        Discard();
        return true;
    }

    public void Discard()
    {
        discardManager.AddToDiscard(card);
        cardImage.enabled = false;
        card = null;
    }
}
