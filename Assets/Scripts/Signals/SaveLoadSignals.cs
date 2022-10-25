using System;
using Data.ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<int> onSaveLevelId = delegate {  };
        public UnityAction<int> onSaveLeft = delegate {  };
        public UnityAction<MoneyData> onTotalMoney = delegate {  };
        
        public Func<int> onLoadLevelId;
        public Func<int> onLoadLeft;
        public Func<MoneyData> onLoadMoney;

    }
}