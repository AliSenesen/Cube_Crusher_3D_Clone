﻿using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers.Cube
{
    public class EnemyCubeMeshController : MonoBehaviour
    {
        [SerializeField] private List<Material> materialList = new List<Material>();
        private Renderer _renderer;
        private float _scale;
        private EnemyCubeManager _enemyCubeManager;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _enemyCubeManager = FindObjectOfType<EnemyCubeManager>();
        }

        private void OnEnable()
        {
            SpawnRandomScale();
            GetMaterial();
        }

        private void GetMaterial()
        {
            if (_scale <= 0.4f)
            {
                _renderer.material.color = materialList[0].color;
            }
            if (_scale>0.4f && _scale <= 0.6f)
            {
                _renderer.material.color = materialList[1].color;
            }
            if (_scale>0.6f && _scale <= 0.8f)
            {
                _renderer.material.color = materialList[2].color;
            }
            if (_scale>0.8f&& _scale <= 1.2f)
            {
                _renderer.material.color = materialList[3].color;
            }
            if (_scale>1.2f&& _scale <= 1.4f)
            {
                _renderer.material.color = materialList[4].color;
            }
            if (_scale>1.4f&& _scale <= 1.6f)
            {
                _renderer.material.color = materialList[5].color;
            }
        }
        
        private void SpawnRandomScale()
        {
            float tempScale = Random.Range(0.4f,1.6f);
            _scale = tempScale;
            transform.DOScaleY(tempScale, 3f).SetEase(Ease.OutElastic);
        }

        public void ArmyHitEnemyCube()
        {
            float tempScale = _scale - 0.1f;
            _scale = tempScale;
            transform.DOScaleY(tempScale, 0.001f).SetEase(Ease.OutElastic).From(_scale);
            GetMaterial();
            if (_scale <= 0.2)
            {
                _enemyCubeManager.RemoveEnemyCubeList(GetComponentInParent<EnemyCube>());
                _enemyCubeManager.ReturnToPoolArmy(transform.parent.gameObject);
            }
        }
    }
}