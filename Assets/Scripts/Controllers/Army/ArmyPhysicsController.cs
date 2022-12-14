using System.Threading.Tasks;
using Controllers.Cube;
using Data.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Army
{
    public class ArmyPhysicsController : MonoBehaviour
    {
        
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject colorParticle;
        [SerializeField] private GameObject coinParticle;
        
        #endregion

        #region Private Variables

        private ArmyManager _armyManager;
        private MoneyData _moneyData;
        
        #endregion

        #endregion
        
        private void Awake()
        {
            _armyManager = FindObjectOfType<ArmyManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyCube"))
            {
             
            GameObject ColorParticle =   
                Instantiate(colorParticle, transform.position + new Vector3(0, 0, -0.25f),
                    Quaternion.identity);
                ColorParticle.transform.SetParent(other.transform);
                EnemyCubeSignals.Instance.onHitEnemyCube?.Invoke(other.transform);
                _armyManager.ReturnToPoolArmy(gameObject);
                _armyManager.ArmyCheck();
            }


            if (other.CompareTag("EnemyBase"))
            {
                GameObject CoinParticle = 
                    Instantiate(coinParticle, transform.position + new Vector3(0, .5f, -0.25f),
                    Quaternion.identity);
                CoinParticle.transform.SetParent(other.transform);
                _armyManager.ReturnToPoolArmy(gameObject);
                _armyManager.ArmyCheck();
                UISignals.Instance.onSetCoinText?.Invoke();
            }

            if (other.TryGetComponent(out IncrementCubes ıncrementCubes))
            {
                StartCoroutine(_armyManager.SpawnArmyInIncrementCube(ıncrementCubes));
                ColliderEnabled(other);
            }
        }

        private async void ColliderEnabled(Collider collider)
        {
            collider.enabled = false;
            await Task.Delay(5000);
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }
}