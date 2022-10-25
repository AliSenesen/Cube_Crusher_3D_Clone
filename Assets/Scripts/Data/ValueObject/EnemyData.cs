using System;
using Abstract;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyData : SaveableEntitiy
    {
        public string Key = "EnemyDataKey";
        
        public int LeftCubeCount;
        public int SpawnCubeCount;
        public int TempLeftCubeCount;


        public EnemyData()
        {
            
        }
        public EnemyData(int leftCubeCount, int spawnCubeCount, int tempLeftCubeCount)
        {
            LeftCubeCount = leftCubeCount;
            SpawnCubeCount = spawnCubeCount;
            TempLeftCubeCount = tempLeftCubeCount;
        }
        public override string GetKey()
        {
            return Key;
        }
    }
}