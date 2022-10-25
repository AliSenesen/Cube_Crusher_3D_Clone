using System;
using Abstract;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData: SaveableEntitiy
    {
        public string Key = "LevelData";
        
        public int LevelId;
        public GameObject LevelGameObject;
        public override string GetKey()
        {
            return Key;
        }
    }
}