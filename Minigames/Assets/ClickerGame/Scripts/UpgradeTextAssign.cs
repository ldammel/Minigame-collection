using System;
using TMPro;
using UnityEngine;

namespace ClickerGame.Scripts
{
    public class UpgradeTextAssign : MonoBehaviour
    {
        [SerializeField] private UpgradeItem upgrade;
        public TextMeshProUGUI CostText;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI PowerText;
        
        private void Update()
        {
            CostText.text = "Cost: $" + WordNotations.WordNotation(upgrade.Cost, "F2");
            LevelText.text = "Level: " + upgrade.level;
            if (upgrade.perSecond)
            {
                PowerText.text =  WordNotations.WordNotation(upgrade.Power, "F2") + " /second";
            }
            else
            {
                PowerText.text =  WordNotations.WordNotation(upgrade.Power+1, "F2") + " /click";
            }
        }
    }
}
