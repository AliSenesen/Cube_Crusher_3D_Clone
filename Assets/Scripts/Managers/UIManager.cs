using Controllers.UI;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Interface;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour,ISaveable
    {
        #region Public Variables

        public MoneyData MoneyData;

        #endregion
        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI leftText;
        [SerializeField] private TextMeshProUGUI coinText;
        
        [SerializeField] private TextMeshProUGUI powerButtonLevelText;
        [SerializeField] private TextMeshProUGUI powerButtonCoinText;
        
        [SerializeField] private TextMeshProUGUI coinButtonLevelText;
        [SerializeField] private TextMeshProUGUI coinButtonCoinText;

        #endregion

        #region Private Variables
        
        private GameManager _gameManager;
        private int _uniqueId = 0;

        #endregion
        
        private void Start()
        {
            GetDataResources();
            SetData();
        }

        private void GetDataResources()
        {
            _gameManager = FindObjectOfType<GameManager>();
            MoneyData = GetMoneyData();
            coinText.text = MoneyData.TotalMoney.ToString();
            powerButtonLevelText.text = "Level " + MoneyData.PowerLevel.ToString();
            powerButtonCoinText.text = MoneyData.PowerMoneyDecrease.ToString();
            coinButtonLevelText.text = "Level " + MoneyData.GainCoinLevel.ToString();
            coinButtonCoinText.text = MoneyData.GainCoinDecrease.ToString();
        }
        
        private void SetData()
        {
            if (!ES3.FileExists($"MoneyDataKey{_uniqueId}.es3"))
            {
                if (!ES3.KeyExists("MoneyDataKey"))
                {
                    MoneyData = GetMoneyData();
                    Save(_uniqueId);
                }
            }
            Load(_uniqueId);
        }
        

        private MoneyData GetMoneyData() => Resources.Load<CD_Money>("Data/CD_Money").MoneyData;
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            UISignals.Instance.onSetLeftText += OnSetLeftText;
            UISignals.Instance.onSetCoinText += OnSetCoinText;

            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            
            LevelSignals.Instance.onLevelInitialize += OnLoad;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }
        private void UnSubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            UISignals.Instance.onSetLeftText -= OnSetLeftText;
            UISignals.Instance.onSetCoinText -= OnSetCoinText;

            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            
            LevelSignals.Instance.onLevelInitialize += OnLoad;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        #endregion

        private void OnSave()
        {
            Save(_uniqueId);
        }

        private void OnLoad()
        {
            Load(_uniqueId);
        }
        
        public void PlayButton()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Playing);
        }

        public void BaseCubePowerIncrease()
        {
            if (MoneyData.TotalMoney - MoneyData.PowerMoneyDecrease < 0)
            {
                return;
            }
            MoneyData.BaseCubeValue += 1;
            BaseCubeSignals.Instance.onBaseCubePowerIncrease?.Invoke();
            MoneyData.TotalMoney -= MoneyData.PowerMoneyDecrease;
            MoneyData.PowerMoneyDecrease += MoneyData.PowerMoneyDecrease;
            MoneyData.PowerLevel += 1;

            powerButtonCoinText.text = MoneyData.PowerMoneyDecrease.ToString();
            powerButtonLevelText.text = "Level " + MoneyData.PowerLevel.ToString();
            coinText.text = MoneyData.TotalMoney.ToString();
            Save(_uniqueId);
        }

        public void GainMoneyIncrease()
        {
            if (MoneyData.TotalMoney - MoneyData.GainCoinDecrease < 0)
            {
                return;
            }
            MoneyData.GainMoney += 1;
            MoneyData.TotalMoney -= MoneyData.GainCoinDecrease;
            MoneyData.GainCoinDecrease += MoneyData.GainCoinDecrease;
            MoneyData.GainCoinLevel += 1;

            coinButtonCoinText.text = MoneyData.GainCoinDecrease.ToString();
            coinButtonLevelText.text = "Level " + MoneyData.GainCoinLevel.ToString();
            coinText.text = MoneyData.TotalMoney.ToString();
            Save(_uniqueId);
        }
        public void RestartButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.GameOpen);
        }

        public void NextLevelButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.GameOpen);
        }
        
        private void OnOpenPanel(UIPanels panel)
        {
            uiPanelController.OpenPanel(panel);
            Save(_uniqueId);
        }
        
        private void OnClosePanel(UIPanels panel)
        {
            uiPanelController.ClosePanel(panel);
        }
        
        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        
        
        private void OnSetLevelText(int levelID)
        {
            if (levelID == 0)
            {
                levelID = 1;
                levelText.text = "Level " + levelID.ToString();
            }
            else
            {
                levelText.text = "Level " + levelID.ToString();
            }
        }
        
        private void OnSetLeftText(int leftTextValue)
        {
            leftText.text = "Left: " + leftTextValue.ToString();
        }
        
        private void OnSetCoinText()
        {
            MoneyData.TotalMoney += MoneyData.GainMoney;
            coinText.text = MoneyData.TotalMoney.ToString();
        }
        
        private void OnLevelFailed()
        {
            OnOpenPanel(UIPanels.FailPanel);
        }

        private void OnNextLevel()
        {
            OnOpenPanel(UIPanels.WinPanel);
        }

        private void OnReset()
        {
            Save(_uniqueId);
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
            
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            
        }

        public void Save(int uniqueId)
        {
            MoneyData = new MoneyData(MoneyData.TotalMoney,
                    MoneyData.GainMoney,
                        MoneyData.BaseCubeValue,
                            MoneyData.PowerMoneyDecrease,
                                MoneyData.PowerLevel,
                                    MoneyData.GainCoinLevel,
                                        MoneyData.GainCoinDecrease);
                
            SaveSignals.Instance.onSaveMoneyData?.Invoke(MoneyData, uniqueId);
        }

        public void Load(int uniqueId)
        {
            MoneyData moneyData = SaveSignals.Instance.onLoadMoneyData?.Invoke(MoneyData.Key, uniqueId);
            
            MoneyData.TotalMoney = moneyData.TotalMoney;
            MoneyData.GainMoney = moneyData.GainMoney;
            MoneyData.BaseCubeValue = moneyData.BaseCubeValue;
            MoneyData.PowerMoneyDecrease = moneyData.PowerMoneyDecrease;
            MoneyData.PowerLevel = moneyData.PowerLevel;
            MoneyData.GainCoinLevel = moneyData.GainCoinLevel;
            MoneyData.GainCoinDecrease = moneyData.GainCoinDecrease;
        }
    }
}