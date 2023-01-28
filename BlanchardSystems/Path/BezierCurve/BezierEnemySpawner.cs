using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BlanchardSystems.Path.BezierCurve
{
    public class BezierEnemySpawner : MonoBehaviour
    {
        [SerializeField] private bool _spawnAllOnStart;
        private static int _qtyEnemiesDied;

        [SerializeField] private EnemyBezierPathRouteSpawner[] _spawners;
        private static int _totalEnemiesToSpawn;
        
        [SerializeField] private UnityEvent _onAllEnemiesDied;

        private void OnEnable()
        {
            _totalEnemiesToSpawn = 0;
            _qtyEnemiesDied = 0;
        }

        private void Start()
        {
            foreach (var spawner in _spawners)
            {
                _totalEnemiesToSpawn += spawner.QtySpawn;
            }
            
            if (_spawnAllOnStart)
            {
                foreach (var spawner in _spawners)
                {
                    var spawnLeft = spawner.QtySpawn;
                    var currentRoute = 0;
                    var currentQtyToSpawn = spawner.QtySpawn/(spawner.BezierPathRoutes.Routes.Length);
                    var count = 0;
                    for (int i = 0; i < spawner.QtySpawn; i++)
                    {
                        spawnLeft--;
                        count++;
                        SpawnEnemy(spawner, false, count/(currentQtyToSpawn*1f), currentRoute); 
                        if (count >= currentQtyToSpawn)
                        {
                            count = 0;
                            currentRoute++;
                            if (currentRoute >= spawner.BezierPathRoutes.Routes.Length) break;
                            currentQtyToSpawn = spawnLeft/(spawner.BezierPathRoutes.Routes.Length - currentRoute);
                        }
                    }   
                }
            }
        }

        private void SpawnEnemy(EnemyBezierPathRouteSpawner spawner, bool atFirstPosition, float customTParam = 0, int customRoute = 0)
        {
            var enemy = Instantiate(spawner.EnemyPrefab, spawner.BezierPathRoutes.transform);
            var enemyFollowerComponent = enemy.GetComponent<BezierFollowPathRoute>();
            enemyFollowerComponent.Initialize(spawner.BezierPathRoutes, customTParam, customRoute);
            enemyFollowerComponent.StartMove();
        }

        public void AddCountEnemyDied()
        {
            _qtyEnemiesDied++;
            VerifyIfAllEnemiesDied();
        }
        
        public void VerifyIfAllEnemiesDied()
        {
            Debug.Log($"EnemiesDied: {_qtyEnemiesDied} , TotalEnemies: {_totalEnemiesToSpawn}");
            if (_qtyEnemiesDied >= _totalEnemiesToSpawn)
            {
                _onAllEnemiesDied.Invoke();
            }
        }

        [Serializable]
        public class EnemyBezierPathRouteSpawner
        {
            public int QtySpawn;
            public BezierPathRoute BezierPathRoutes;
            public GameObject EnemyPrefab;
        }
        
    }
   
}