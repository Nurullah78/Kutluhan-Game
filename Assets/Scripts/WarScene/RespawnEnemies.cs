using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace WarScene
{
    public class RespawnEnemies : MonoBehaviour
    {

//----------------- Nesneler -----------------

        [SerializeField]
         List<GameObject> pooledEnemies;
        [SerializeField]
         List<Vector3> pooledEnemiesBasePoint;

        GameObject RespawnObject;

        int poolSize, childIndex;


//----------------- Metotlar -----------------

        void PoolEnemies()
        {
            pooledEnemies = new List<GameObject>();
            pooledEnemiesBasePoint = new List<Vector3>();

            for (int i = 0; i < poolSize; i++)
            {
                pooledEnemies.Add(transform.GetChild(i).gameObject);
                pooledEnemiesBasePoint.Add(transform.GetChild(i).position);
            }
        }

        void Start()
        {
            poolSize = transform.childCount;
            PoolEnemies();
        }

        IEnumerator Respawn(Transform enemy)
        {
            enemy.position = pooledEnemiesBasePoint[childIndex];

            yield return new WaitForSeconds(8f);

            enemy.gameObject.SetActive(true);

            enemy.GetComponent<CharacterState>().Respawner();

            RespawnObject = enemy.GetComponent<CharacterState>().spawnEffect.gameObject;
            RespawnObject.SetActive(true);

            yield return null;
        }

        void Update()
        {
            foreach (GameObject enemy in pooledEnemies)
            {
                childIndex = enemy.transform.GetSiblingIndex();

                if (!enemy.activeSelf)
                {
                    StartCoroutine(Respawn(enemy.transform));
                }
            }
        }
    }
}
