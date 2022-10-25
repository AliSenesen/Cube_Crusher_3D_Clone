using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData : SaveableEntitiy
    {
        public string Key = "EnemyLeftData";
        
        public int LeftCubeCount;
        public int SpawnCubeCount;
        public int TempLeftCubeCount;
        public override string GetKey()
        {
            return Key;
        }
    }
}