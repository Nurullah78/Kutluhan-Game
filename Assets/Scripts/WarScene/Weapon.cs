using UnityEngine;
using Core;

namespace WarScene
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]

    public class Weapon : ScriptableObject
    {

//----------------- Nesneler -----------------
        
        [SerializeField]
         AnimatorOverrideController animationOverride = null;
        [SerializeField]
         GameObject primaryPrefab = null, secondaryPrefab = null;
        [SerializeField]
         Bullet bullet = null;
        [SerializeField]
         public float weaponRange = 1.5f, weaponDamage = 1f;
        [SerializeField]
         bool isRightHand = true;

        const string firstWeaponName = "FirstWeapon", secondWeaponName = "SecondWeapon";


        //----------------- Metotlar -----------------

        public bool HasBullet()
        { return bullet != null; }

        public float GetDamage()
        { return weaponDamage; }

        public float GetRange()
        { return weaponRange; }

        void InstantiateWeapon(GameObject weaponPrefab, Transform handTransform, string weaponName)
        {
            GameObject weapon = Instantiate(weaponPrefab, handTransform);
            weapon.name = weaponName;
        }

        public void DestroyOldWeapon(Transform primaryTransform, Transform secondaryTransform)
        {
            Transform oldWeapon = primaryTransform.Find(firstWeaponName);
            if (oldWeapon != null)
            {
                Transform secondWeapon = secondaryTransform.Find(secondWeaponName);
                if (secondWeapon != null)
                {
                    secondWeapon.name = "DESTROYED";
                    Destroy(secondWeapon.gameObject);
                }
            }
            
            if (oldWeapon == null)
                oldWeapon = secondaryTransform.Find(secondWeaponName);

            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYED";
            Destroy(oldWeapon.gameObject);
        }

        public void Spawn(Transform primaryTransform, Transform secondaryTransform, Animator animator, float damageMultiplier)
        {
            if (isRightHand && primaryPrefab != null)
            {
                InstantiateWeapon(primaryPrefab, primaryTransform, firstWeaponName);

                if (secondaryPrefab != null)
                {
                    InstantiateWeapon(secondaryPrefab, secondaryTransform, secondWeaponName);
                }
            }
            else if(secondaryPrefab != null)
                InstantiateWeapon(secondaryPrefab, secondaryTransform, secondWeaponName);

            //DestroyOldWeapon(primaryTransform, secondaryTransform);

            //if (weaponPrefab != null)
            //{
            //    Transform handTransform = WhichHand(primaryTransform, secondaryTransform);
            //    GameObject weapon = Instantiate(weaponPrefab, handTransform);
            //    weapon.name = weaponName;
            //}

            if (animationOverride != null)
                animator.runtimeAnimatorController = animationOverride;
        }

        Transform WhichHand(Transform primaryTransform, Transform secondaryTransform)
        {
            Transform handTransform;

            if (isRightHand)
                handTransform = primaryTransform;
            else
                handTransform = secondaryTransform;

            return handTransform;
        }

        public void LaunchBullet(Transform primaryTransform, Transform secondaryTransform, CharacterState target)
        {
            Transform handTransform = WhichHand(primaryTransform, secondaryTransform);

            Bullet bulletInstance = Instantiate(bullet, handTransform.position, Quaternion.identity);
            bulletInstance.SetTarget(target, weaponDamage);
        }
    }
}
