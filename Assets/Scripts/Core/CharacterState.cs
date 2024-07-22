using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Animations;
using WarScene;
using Movement;

namespace Core
{
    public class CharacterState : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
         UnityEvent onDamage, onDie;

        [NonSerialized]
         public bool isLeveledUp, isHealthHealing, isManaHealing;

        public static CharacterState Instance { get; set; }

        public SpawnEffect spawnEffect;
        AnimationController animationController => GetComponent<AnimationController>();
        GameObject Player => GameObject.FindWithTag("Player");

        bool isFighting, isDead; //, isMoving;

        float deadTime;//timeCounter, currentTime;


        // ---- Karakterin Güncel Değerleri ---- //
        [NonSerialized]
        public float currentHealth, currentMana,
            currentExp; //, currentEnergy;


        // ----- Karakterin Sınır Değerleri ----- //
        public float maxHealth, maxMana,
            requiredExp; //, maxEnergy;

        
//---------------Metotlar---------------

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            spawnEffect.gameObject.SetActive(false);
            currentHealth = maxHealth;
            currentMana = maxMana;
            //currentEnergy = maxEnergy;
        }
        
        public bool IsDead()
        {
            return isDead;
        }

        public void Respawner()
        {
            currentHealth = maxHealth;

            if (gameObject.CompareTag("Player"))
            {
                GetComponent<MoveCharacter>().enabled = true;
                GetComponent<SelectEnemy>().enabled = true;
            }

            GetComponent<CapsuleCollider>().enabled = true;
            GetComponent<NavMeshAgent>().enabled = true;
            isDead = false;
        }

        void Die()
        {
            onDie.Invoke();
            isDead = true;
            animationController.AnimSetDieTrigger();
            
            if (gameObject.CompareTag("Player"))
            {
                GetComponent<MoveCharacter>().enabled = false;
                GetComponent<SelectEnemy>().enabled = false;
            }

            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        public void EnemyTakeDamage(float damage)
        {
            if (IsDead())
                return;
            
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            onDamage.Invoke();

            transform.LookAt(Player.transform);

            if (!isFighting)
            {
                isFighting = true;
                animationController.AnimSetIsFighting(true);
                animationController.AnimSetFisting(true);
            }

            if (currentHealth <= 0)
                Die();
        }
        
        public void PlayerTakeDamage(float damage)
        {
            if (IsDead())
                return;
            
            currentHealth = Mathf.Max(currentHealth - damage, 0);
            onDamage.Invoke();

            if (currentHealth <= 0)
                Die();
        }

        public IEnumerator StartHealthHealing()
        {
            isHealthHealing = true;

            while (currentHealth < maxHealth)
            {
                yield return new WaitForSeconds(5);
                
                if (gameObject.CompareTag("Player"))
                    currentHealth += maxHealth / 25;

                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                    break;
                }
            }

            isHealthHealing = false;
            yield return null;
        }
        
        public IEnumerator StartManaHealing()
        {
            isManaHealing = true;

            while (currentMana < maxMana)
            {
                yield return new WaitForSeconds(8);
                
                if (gameObject.CompareTag("Player"))
                    currentMana += maxMana / 35;

                if (currentMana >= maxMana)
                {
                    currentMana = maxMana;
                    break;
                }
            }

            isManaHealing = false;
            yield return null;
        }
        
        public void StopHealthHealing()
        {
            StopCoroutine(StartHealthHealing());
        }
        
        public void ExpGain()
        {
            currentExp += 20;

            if (currentExp >= requiredExp)
            {
                isLeveledUp = true;
                currentExp -= requiredExp;
            }
        }

        //void EnergyDecreace()
        //{
        //    timeCounter += Time.deltaTime;

        //    if (isMoving)
        //    {
        //        currentTime = timeCounter;
        //        if (timeCounter >= 8)
        //        {
        //            timeCounter = 0;
        //            currentEnergy -= 1;
        //        }
        //    }
        //    else
        //    {
        //        if (timeCounter >= 20 - currentTime)
        //        {
        //            timeCounter = 0;
        //            currentEnergy -= 1;
        //        }
        //    }
        //}

        void Update()
        {
            if (IsDead())
            {
                deadTime += Time.deltaTime;

                if (deadTime >= 4)
                    gameObject.SetActive(false);

                return;
            }

            if (currentMana >= maxMana)
            {
                currentMana = maxMana;
            }

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (gameObject.CompareTag("Player"))
            {
                if (!isManaHealing)
                    StartCoroutine(StartManaHealing());
            }

            //EnergyDecreace();

            // İksir barı test tuşu
            if (CompareTag("Player") && Input.GetKeyDown(KeyCode.M))
            {
                currentMana -= 5;
            }

            // Tecrübe barı test tuşu
            if (CompareTag("Player") && Input.GetKeyDown(KeyCode.L))
            {
                currentExp += 15;
            }
        }
    }
}
