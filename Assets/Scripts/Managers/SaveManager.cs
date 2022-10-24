using Commands.SaveLoad;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveLevelData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadLevelData += _loadGameCommand.Execute<LevelData>;
            
            SaveSignals.Instance.onSaveEnemyData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadEnemyData += _loadGameCommand.Execute<EnemyData>;
            
            SaveSignals.Instance.onSaveMoneyData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadMoneyData += _loadGameCommand.Execute<MoneyData>;
        }

        private void UnSubscribeEvents()
        {
            SaveSignals.Instance.onSaveLevelData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadLevelData -= _loadGameCommand.Execute<LevelData>;
            
            SaveSignals.Instance.onSaveEnemyData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadEnemyData -= _loadGameCommand.Execute<EnemyData>;
            
            SaveSignals.Instance.onSaveMoneyData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadMoneyData -= _loadGameCommand.Execute<MoneyData>;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
    }
}