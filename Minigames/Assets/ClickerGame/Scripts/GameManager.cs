using System;
using TMPro;
using UnityEngine;

namespace ClickerGame.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Base Settings")]
        [SerializeField] private Values values;
        [SerializeField] private UpgradeItem[] upgrades;
        [Space]
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI perClickText;
        public TextMeshProUGUI perSecondText;
        
        [Header("Offline Settings")]
        public DateTime currentDate;
        public DateTime oldTime;
        public int offlineProgressionCheck;
        public float idleTime;
        public TextMeshProUGUI offlineTimeText;
        public float saveTime;
        public GameObject offlineBox;
        
        [Header("Username Settings")]
        public TMP_InputField usernameInput;
        public string username;
        public TextMeshProUGUI usernameText;
        public GameObject usernameBox;

        [Header("AutoClick")]
        public bool autoClick;

        public float secondsPerClick  = 1;
        private float _timer;
        private void Start()
        {
            offlineBox.SetActive(false);
            Load();
            usernameBox.SetActive(username == "<Username>");
            LoadOfflineProduction();
        }

        private void Update()
        {
            moneyText.text = "$" + WordNotations.WordNotation(values.money, "F2");
            perClickText.text = WordNotations.WordNotation(values.moneyPerClick, "F2") + " Per Click";
            perSecondText.text = WordNotations.WordNotation(values.moneyPerSecond, "F2") + " Per Second";

            values.money += values.moneyPerSecond * Time.deltaTime;

            usernameText.text = username;

            saveTime += Time.deltaTime;

            if (autoClick)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    Click();
                    _timer = secondsPerClick;
                }
            }
            if (saveTime >= 5)
            {
                saveTime = 0;
                Save();
            }
        }

        public void Click()
        {
            values.Click();
        }

        public void Username()
        {
            username = usernameInput.text;
            usernameText.text = username;
        }

        public void CloseUsernameBox()
        {
            usernameBox.SetActive(false);
        }

        public void BuyUpgrade(UpgradeItem upgrade)
        {
            if (!(values.money >= upgrade.Cost)) return;
            UpgradeDefaults(ref upgrade.level, upgrade.Cost);
            upgrade.Upgrade();
        }

        public void UpgradeDefaults(ref int level, double cost)
        {
            values.money -= cost;
            level++;
        }

        public void Reset()
        {
            values.money = 0;
            values.moneyPerSecond = 0;
            values.moneyPerClick = 1;
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].level = 0;
            }
        }

        public void AutoClick(bool state)
        {
            autoClick = state;
        }
        
        private void Save()
        {
            offlineProgressionCheck = 1;
        
            PlayerPrefs.SetString("money", values.money.ToString());
            PlayerPrefs.SetString("mpc", values.moneyPerClick.ToString());
            PlayerPrefs.SetString("mps", values.moneyPerSecond.ToString());
            PlayerPrefs.SetString("username", username);
            for (int i = 0; i < upgrades.Length; i++)
            {
                string upgradeLevelName = "Level" + i;
                PlayerPrefs.SetInt(upgradeLevelName, upgrades[i].level);
            }
            PlayerPrefs.SetInt("OfflineProgressionCheck", offlineProgressionCheck);
        
            PlayerPrefs.SetString("OfflineTime",DateTime.Now.ToBinary().ToString());
        }
        
        private void Load()
        {
            values.money = double.Parse(PlayerPrefs.GetString("money", "0"));
            values.moneyPerClick = double.Parse(PlayerPrefs.GetString("mpc","1"));
            values.moneyPerSecond = double.Parse(PlayerPrefs.GetString("mps","0"));
            for (int i = 0; i < upgrades.Length; i++)
            {
                string upgradeLevelName = "Level" + i;
                upgrades[i].level = PlayerPrefs.GetInt(upgradeLevelName, 0);
            }
            offlineProgressionCheck = PlayerPrefs.GetInt("OfflineProgressionCheck", 0);
            username = PlayerPrefs.GetString("username", "<Username>");
            LoadOfflineProduction();
        }

        private void LoadOfflineProduction()
        {
            if (offlineProgressionCheck == 1)
            {
                offlineBox.SetActive(true);
                long previousTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
                oldTime = DateTime.FromBinary(previousTime);
                currentDate = DateTime.Now;
                TimeSpan difference = currentDate.Subtract(oldTime);
                idleTime = (float) difference.TotalSeconds;

                var moneyToEarn = values.moneyPerSecond * idleTime;
                values.money += moneyToEarn;
                TimeSpan timer = TimeSpan.FromSeconds(idleTime);

                var offlineMoneyText = WordNotations.WordNotation(moneyToEarn, "F2");

                offlineTimeText.text = "You were gone for: " + timer.ToString(@"hh\:mm\:ss") + "\n\nYou earned: $" + offlineMoneyText;
            }
        }
    
        public void CloseOfflineBox()
        {
            offlineBox.SetActive(false); 
        }
    }
}

