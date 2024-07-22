using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarScene;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]
     Weapon unarmed, sword;
    [SerializeField]
     GameObject player;

    bool isTap;

    public void ChangeWeapon()
    {
        isTap = !isTap;

        if (!isTap)
        {
            player.GetComponent<Fighter>().SpawnWeapon(unarmed);
        }
        else
            player.GetComponent<Fighter>().SpawnWeapon(sword);
    }
}
