using System;
using Data.ValueObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<LevelData, int> onSaveLevelData = delegate { };
        public UnityAction<MoneyData, int> onSaveMoneyData = delegate { };
        public UnityAction<EnemyData, int> onSaveEnemyData = delegate { };
        
        public Func<string, int, LevelData> onLoadLevelData;
        public Func<string, int, MoneyData> onLoadMoneyData;
        public Func<string, int, EnemyData> onLoadEnemyData;

    }
}