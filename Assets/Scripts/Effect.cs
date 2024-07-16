using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HapaMagic
{
    [CreateAssetMenu(fileName = "New Effect", menuName = "Effect")]
    public class Effect : ScriptableObject
    {
        public enum EffectType
        {
            Instant,
            Active,
            Power
        }
        public enum EffectAbility
        {
            None,
            SpawnBasicAnt,
            SpawnHoneyAnt,
            SpawnBeetleAnt,
            SpawnMantisAnt,
            SpawnSoldierAnt,
            Cycle,
            Draw,
            Discard
        }

        public string effectName;
        public string description;
        public EffectType effectType;
        public EffectAbility effectAbility;
    }
}
