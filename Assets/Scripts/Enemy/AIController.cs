using UnityEngine;
using UnityEngine.AI;
using Animations;
using WarScene;
using Movement;
using Core;

namespace Enemy
{
    public class AIController : MonoBehaviour
    {

//----------------- Nesneler -----------------

        [Range(0, 1)] [SerializeField]
         float patrolSpeedFraction = 0.4f;
        [SerializeField]
        float chaseDistance = 5f, suspicionTime = 5f,// maxSpeed = 4.128f,
                waypointTolerance = 1f, waypointLifeTime = 3f;
                //aggroCooldownTime = 5f, shoutDistance = 5f;
        [SerializeField]
         PatrolPath patrolPath;

        GameObject Player => GameObject.FindWithTag("Player");
        AnimationController animationController => GetComponent<AnimationController>();
        NavMeshAgent navMeshAgent => GetComponent<NavMeshAgent>();
        CharacterState isLive => GetComponent<CharacterState>();
        Fighter fighter => GetComponent<Fighter>();
        Mover mover => GetComponent<Mover>();

        Vector3 enemyLocation, nextPosition;

        float timeSinceLastSawPlayer, timeSinceArrivedWaypoint,
            xPos, zPos, xRef, zRef;//, timeSinceAggrevate = Mathf.Infinity;


//----------------- Metotlar -----------------

        void Start()
        {
            xRef = transform.position.x;
            zRef = transform.position.z;
            xPos = Random.Range(xRef - 10, xRef + 10);
            zPos = Random.Range(zRef - 10, zRef + 10);

            nextPosition = new Vector3(xPos, transform.position.y, zPos);

            enemyLocation = transform.position;
            navMeshAgent.destination = gameObject.transform.position;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        float DistanceToPlayer()
        {
            if (Player != null)
                return Vector3.Distance(Player.transform.position, transform.position);
            else
                return chaseDistance + 1;
        }

        bool AtWaypoint()
        {
            float distanceWaypoint = Vector3.Distance(transform.position, GetNextWaypoint());
            return distanceWaypoint < waypointTolerance;
        }

        void CycleWaypoint()
        {
            xPos = Random.Range(xRef - 10, xRef + 10);
            zPos = Random.Range(zRef - 10, zRef + 10);
        }

        Vector3 GetNextWaypoint()
        {
            return new Vector3(xPos, transform.position.y, zPos);
        }

        //public void Aggrevate()
        //{
        //    timeSinceAggrevate = 0;
        //}

        //bool IsAggrevated()
        //{
        //    return DistanceToPlayer() < chaseDistance || timeSinceAggrevate < aggroCooldownTime;
        //}

        //void AggrevateNearbyEnemies()
        //{
        //    RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
        //    foreach (RaycastHit hit in hits)
        //    {
        //        AIController ai = hit.collider.GetComponent<AIController>();
        //        if (ai == null) continue;

        //        ai.Aggrevate();
        //    }
        //}

        void Update()
        {
            if (isLive.IsDead() || Player.GetComponent<CharacterState>().IsDead())
            {
                fighter.Cancel();
                return;
            }

            mover.UpdateAnimator();

            if (DistanceToPlayer() < chaseDistance && fighter.CanAttack(Player))
            {
                timeSinceLastSawPlayer = 0;
                Player.GetComponent<CharacterState>().StopHealthHealing();
                fighter.Attack(Player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                timeSinceLastSawPlayer += Time.deltaTime;
                fighter.Cancel();

                if (!Player.GetComponent<CharacterState>().isHealthHealing)
                    StartCoroutine(Player.GetComponent<CharacterState>().StartHealthHealing());
            }
            else
            {
                nextPosition = enemyLocation;

                if (patrolPath != null)
                {
                    if (AtWaypoint())
                    {
                        timeSinceArrivedWaypoint = 0;
                        CycleWaypoint();
                    }

                    nextPosition = GetNextWaypoint();
                }

                if (timeSinceArrivedWaypoint > waypointLifeTime)
                {
                    mover.StartMoveAction(nextPosition, patrolSpeedFraction);
                }
            }

            //if (IsAggrevated() && fighter.CanAttack(Player))
            //{
            //    maxSpeed = 5.7f;
            //    timeSinceLastSawPlayer = 0;
            //    fighter.Attack(Player);

            //    AggrevateNearbyEnemies();
            //}
            //else if (timeSinceLastSawPlayer < suspicionTime)
            //{
            //    maxSpeed = 2.5f;
            //    GetComponent<ActionScheduler>().CancelCurrentAction();
            //}


            timeSinceArrivedWaypoint += Time.deltaTime;
            //timeSinceAggrevate += Time.deltaTime;
        }
    }
}
