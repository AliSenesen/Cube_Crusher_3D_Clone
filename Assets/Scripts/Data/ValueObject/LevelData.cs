using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData : SaveableEntitiy
    {
        public string Key = "LevelDataKey";
        
        public GameObject LevelGameObject;

        public LevelData()
        {
            
        }
        public LevelData(GameObject levelGameObject)
        {
            LevelGameObject = levelGameObject;
        }
        public override string GetKey()
        {
            return Key;
        }
    }
}