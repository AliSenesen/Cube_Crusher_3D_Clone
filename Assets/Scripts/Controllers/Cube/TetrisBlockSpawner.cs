using System;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Cube
{
    public class TetrisBlockSpawner : MonoBehaviour
    {
        [SerializeField] private List<TetrisBlockManager> tetrisBlockList;
        private GridManager _gridManager;
        private List<TetrisBlockManager> _spawnList = new List<TetrisBlockManager>();
        private TetrisBlockManager spawningObject;

        private void Awake()
        {
            _gridManager = FindObjectOfType<GridManager>();
        }

        void Start()
        {
            DetectSpawnableBlocks();
            RandomSpawnBlock();
            //Debug.Log(FindObjectOfType<LevelSignals>().gameObject.name);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        private void OnChangeGameState(GameStates currentState)
        {
            if (currentState == GameStates.EnemyMovePhase)
            {
                DetectSpawnableBlocks();
                RandomSpawnBlock();
            }
                
                
        }
        
        // public bool SpawnCheck(Vector2Int checkingTileIndex, CubeTransform[] cubePositions)
        // {
        //     bool control = false;
        //     
        //     foreach (CubeTransform cubeTransform in cubePositions)
        //     {
        //         int xIndex = checkingTileIndex.x + cubeTransform.x;
        //         int yIndex = checkingTileIndex.y + cubeTransform.y;
        //
        //         if (xIndex < 0 || 
        //             yIndex < 0 || 
        //             xIndex >= _gridManager._nodes.GetLength(0) || 
        //             yIndex >= _gridManager._nodes.GetLength(1)) return false;
        //         
        //         Tile checkingTile = _gridManager._nodes[xIndex, yIndex];
        //
        //         control = checkingTile.IsPlaceable;
        //         
        //         if (control == false) return false;
        //     }
        //     
        //     return true;
        // }
        public bool SpawnCheck(Vector2Int checkingTileIndex, CubeTransform[] cubePositions)
        {
            bool control = false;
            
            foreach (CubeTransform cubeTransform in cubePositions)
            {
                int xIndex = checkingTileIndex.x + cubeTransform.x;
                int yIndex = checkingTileIndex.y + cubeTransform.y;

                if (xIndex < 0 || 
                    yIndex < 0 || 
                    xIndex >= _gridManager._nodes.GetLength(0) || 
                    yIndex >= _gridManager._nodes.GetLength(1)) return false;
                
                Tile checkingTile = _gridManager._nodes[xIndex, yIndex];

                control = checkingTile.IsPlaceable;
                if (checkingTile.IsEnemyTile) return false;
                if (control == false) return false;
            }
            
            return true;
        }

        private void DetectSpawnableBlocks()
        {
            _spawnList.Clear();
            for (int i = 0; i < tetrisBlockList.Count; i++)
            {
                foreach (Tile tile in _gridManager._nodes)
                {
                    if (SpawnCheck(tile.CellIndex,tetrisBlockList[i].cubePositions))
                    {
                        _spawnList.Add(tetrisBlockList[i]);
                        break;
                    }
                }
            }
        }

        private void RandomSpawnBlock()
        {
            spawningObject = Instantiate(_spawnList[Random.Range(0, _spawnList.Count)]);
            spawningObject.transform.position = transform.position;
        }
    }
}
