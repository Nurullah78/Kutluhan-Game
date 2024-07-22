using System;
using UnityEngine;
using UnityEngine.AI;
using Animations;
using Core;
using Movement;

namespace WarScene
{
    public class SelectEnemy : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
        float maximumSpeed;
        [NonSerialized]
        public bool isSelected;
        [NonSerialized]
        public string enemyName;

        AnimationController animationController => GetComponent<AnimationController>();
        NavMeshAgent navMeshAgent => GetComponent<NavMeshAgent>();
        Fighter fighter => GetComponent<Fighter>();

        CharacterState target;

        float inputMagnitude;
        int pressedTime;


//---------------Metotlar---------------

        void OnTriggerStay(Collider other)
        {
            if(!other.CompareTag("Player"))
                fighter.targetObject = other.gameObject.GetComponent<CharacterState>();
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                fighter.targetObject = null;
        }

        static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void SelectingEnemy(GameObject target)
        {
            if (enemyName == null)
            {
                isSelected = true;
                enemyName = target.name;
                pressedTime++;
            }
            else if (enemyName == target.name)
            {
                pressedTime++;
            }
        }

        bool MoveToEnemy()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                target = hit.transform.GetComponent<CharacterState>();

                if (target == null || target.gameObject.CompareTag("Player"))
                    continue;

                if (target.IsDead())
                {
                    enemyName = null;
                    isSelected = false;
                    target = null;
                    fighter.Cancel();
                }

                if (!fighter.CanAttack(target.gameObject))
                    continue;

                if (Input.GetMouseButtonDown(0))
                {
                    SelectingEnemy(target.gameObject);

                    if (pressedTime == 2)
                    {
                        pressedTime = 0;
                        navMeshAgent.enabled = true;
                        fighter.Attack(target.gameObject);
                    }
                }
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                pressedTime = 0;
            }

            return false;
        }

        void Update()
        {
            MoveToEnemy();
        }
    }
}
