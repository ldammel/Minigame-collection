using System;
using TMPro;
using UnityEngine;

namespace ClickerGame.Scripts
{
    [CreateAssetMenu(fileName = "New Upgrade", menuName = "ClickerGame/Upgrades")]
    public class UpgradeItem : ScriptableObject
    {
        public Values values;

        public float costMultiplier;
        public float powerMultiplier;
        public double Cost => (costMultiplier * Math.Pow(2, level))+Power;
        public int level;
        public double Power => powerMultiplier * level;
        public bool perSecond;

        public void Upgrade()
        {
            if (perSecond)
            {
                values.moneyPerSecond += Power;
            }
            else
            {
                values.moneyPerClick += 1 + Power;
            }
        }
    }
}