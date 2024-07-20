using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace HapaMagic {
        
    [CreateAssetMenu(fileName = "New Ant", menuName = "Ant")] 
    public class Ant : ScriptableObject {
        public int currHealth;
        public int maxHealth;
        public int attack;
        public int moveSpeed;
        public Sprite antSprite;
    }
}
