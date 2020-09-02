using System;
using UnityEngine;

namespace ClickerGame.Scripts
{
    [CreateAssetMenu(fileName = "New Value", menuName = "ClickerGame/Values")]
    public class Values : ScriptableObject
    {
        public double money;
        public double moneyPerClick;
        public double moneyPerSecond;

        public void Click()
        {
            money += moneyPerClick;
        }
    }
}
