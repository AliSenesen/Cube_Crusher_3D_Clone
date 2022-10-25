using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class MoneyData : SaveableEntitiy
    {
        public string Key = "MoneyDataKey";
        
        public int TotalMoney;
        
        public int GainMoney;
        public int BaseCubeValue;
        
        public int PowerMoneyDecrease;
        public int PowerLevel;
        
        public int GainCoinLevel;
        public int GainCoinDecrease;

        public MoneyData()
        {
            
        }
        public MoneyData(int totalMoney, int gainMoney, int baseCubeValue, int powerMoneyDecrease, int powerLevel,
            int gainCoinLevel, int gainCoinDecrease)
        {
            TotalMoney = totalMoney;
            GainMoney = gainMoney;
            BaseCubeValue = baseCubeValue;
            PowerMoneyDecrease = powerMoneyDecrease;
            PowerLevel = powerLevel;
            GainCoinLevel = gainCoinLevel;
            GainCoinDecrease = gainCoinDecrease;
        }
        
        public override string GetKey()
        {
            return Key;
        }
    }
}