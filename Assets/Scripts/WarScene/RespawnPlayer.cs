using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace WarScene
{
    public class RespawnPlayer : MonoBehaviour
    {

//----------------- Nesneler -----------------

        [SerializeField]
         Vector3 playerBasePoint;

        Transform player;
        GameObject RespawnObject;


//----------------- Metotlar -----------------

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerBasePoint = player.position;
        }

        IEnumerator Respawn(Transform player)
        {
            player.position = playerBasePoint;

            yield return new WaitForSeconds(3f);

            player.gameObject.SetActive(true);

            player.GetComponent<CharacterState>().Respawner();

            RespawnObject = player.GetComponent<CharacterState>().spawnEffect.gameObject;
            RespawnObject.SetActive(true);

            yield return null;
        }

        void Update()
        {
            if (!player.gameObject.activeSelf)
            {
                StartCoroutine(Respawn(player));
            }
        }
    }
}
