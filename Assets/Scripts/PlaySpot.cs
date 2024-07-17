using System.Collections;
using System.Collections.Generic;
using HapaMagic;
using UnityEngine;

public class PlaySpot : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    HomeNest homeNest;

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
            switch (effect.effectAbility) {
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
            
            return true;
        }
        return false;
    }
}
