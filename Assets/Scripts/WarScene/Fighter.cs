using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Animations;
using Movement;
using Core;

namespace WarScene
{
    public class Fighter : MonoBehaviour, IAction
    {

//----------------- Nesneler -----------------

        [SerializeField]
         Transform primaryTransform = null, secondaryTransform = null;
        [SerializeField]
         public Weapon defaultWeapon = null;
        [SerializeField]
         float damageMultiplier = 1f;


        [NonSerialized]
         public CharacterState targetObject;

        AnimationController animationController => GetComponent<AnimationController>();
        NavMeshAgent navMeshAgent => GetComponent<NavMeshAgent>();
        Mover mover => GetComponent<Mover>();

        bool isFisting, isFighting;
        float attackTime;


//----------------- Metotlar -----------------

        public void SpawnWeapon(Weapon weapon)
        {
            defaultWeapon.DestroyOldWeapon(primaryTransform, secondaryTransform);

            defaultWeapon = weapon;

            Animator animator = GetComponent<Animator>();
            defaultWeapon.Spawn(primaryTransform, secondaryTransform, animator, damageMultiplier);
        }

        void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

        public void Attack(GameObject target)
        {
            targetObject = target.GetComponent<CharacterState>();
        }

        bool GetIsInRange()
        {
            if (targetObject != null)
                return Vector3.Distance(transform.position, targetObject.transform.position) <= defaultWeapon.GetRange();
            else
                return true;
        }

        public IEnumerator SetAttack()
        {
            animationController.AnimSetIsFighting(true);
            animationController.AnimSetFisting(true);
            yield return new WaitForSeconds(0.28f);

            isFighting = true;
            isFisting = true;
            yield return null;
        }

        public IEnumerator FistAgain()
        {
            attackTime = 0;
            yield return new WaitForSeconds(0.65f);
            animationController.AnimSetFisting(true);
            isFisting = true;
        }

        public void AttackMethod()
        {
            if (isFighting)
            {
                if (targetObject != null)
                    transform.LookAt(targetObject.transform.position);

                if (isFisting)
                {
                    if (attackTime / 5.3f <= 1.23f)
                    {
                        attackTime += Time.deltaTime;
                    }
                    else
                    {
                        animationController.AnimSetFisting(false);
                        isFisting = false;
                        attackTime = 0;
                    }
                }
                else
                {
                    StartCoroutine(FistAgain());
                }
            }
            else
                StartCoroutine(SetAttack());
        }

        void StopAttack()
        {
            animationController.AnimSetFisting(false);
            animationController.AnimSetIsFighting(false);
            isFisting = false;
            isFighting = false;
            attackTime = 0;
        }

        public void Cancel()
        {
            targetObject = null;
            StopAttack();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            CharacterState isLive = GetComponent<CharacterState>();
            return isLive != null && !isLive.IsDead();
        }

        void Hit()
        {
            if (targetObject == null)
                return;

            if (defaultWeapon.HasBullet())
            {
                defaultWeapon.LaunchBullet(primaryTransform, secondaryTransform, targetObject);
            }
            else
            {
                if (targetObject.CompareTag("Player"))
                    targetObject.PlayerTakeDamage(defaultWeapon.GetDamage());
                else
                    targetObject.EnemyTakeDamage(defaultWeapon.GetDamage());
        }
    }
        
        void ComboHit()
        {
            if (targetObject == null)
                return;

            if (defaultWeapon.HasBullet())
            {
                defaultWeapon.LaunchBullet(primaryTransform, secondaryTransform, targetObject);
            }
            else
            {
                if (targetObject.CompareTag("Player"))
                    targetObject.PlayerTakeDamage(defaultWeapon.GetDamage() * 2);
                else
                    targetObject.EnemyTakeDamage(defaultWeapon.GetDamage() * 2);
            }
        }

        void MoveOrAttack()
        {
            if (!GetIsInRange())
            {
                mover.StartMoveAction(targetObject.transform.position, 0.75f);
            }
            else
            {
                mover.Cancel();
                AttackMethod();
            }
        }

        void Update()
        {
            if (targetObject != null && targetObject.IsDead())
            {
                Cancel();

                if (CompareTag("Player"))
                {
                    GetComponent<CharacterState>().ExpGain();
                    if (!GetComponent<CharacterState>().isHealthHealing)
                        StartCoroutine(GetComponent<CharacterState>().StartHealthHealing());
                }
            }

            if (targetObject == null)
                return;
            
            if (navMeshAgent.enabled)
            {
                if(gameObject.CompareTag("Player"))
                {
                    if (GetComponent<SelectEnemy>().isSelected)
                    {
                        MoveOrAttack();
                    }
                }
                else
                {
                    MoveOrAttack();
                }
            }
        }
    }
}
