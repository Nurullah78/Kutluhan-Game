using UnityEngine;
using UnityEngine.AI;
using Animations;
using WarScene;

namespace Movement
{
    public class Mover : MonoBehaviour, IAction
    {

//---------------Nesneler---------------

        [SerializeField]
         float maxSpeed = 2.75f;

        AnimationController animationController => GetComponent<AnimationController>();
        NavMeshAgent navMeshAgent => GetComponent<NavMeshAgent>();
        Fighter fighter => GetComponent<Fighter>();


        //---------------Metotlar---------------

        public void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float forwardSpeed = Mathf.Clamp01(localVelocity.magnitude); //Düzleme aktarılan değerlerin uzunluğunu(magnitude), 0 ile 1 arasında değer döndürerek(Clamp01), değişkene aktarır

            if (!gameObject.CompareTag("Player"))
                forwardSpeed *= GetComponent<NavMeshAgent>().speed;

            animationController.AnimSetInputMagnitude(forwardSpeed); //Animator bileşeninin içerisindeki parametrenin değerini günceller

            if (forwardSpeed != 0)
                animationController.AnimSetIsMoving(true);
            else
                animationController.AnimSetIsMoving(false);
        }

        public void MoveTo(Vector3 hit, float speedFraction)
        {
            navMeshAgent.speed = maxSpeed * speedFraction;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = hit;
        }
        
        public void StartMoveAction(Vector3 hit, float speedFraction)
        {
            animationController.AnimSetIsFighting(false);
            animationController.AnimSetFisting(false);
            MoveTo(hit, speedFraction);

            if (GetComponent<CharacterController>() != null)
                GetComponent<CharacterController>().enabled = false;
        }

        public void Cancel()
        {
            animationController.AnimSetIsMoving(false);
            navMeshAgent.destination = gameObject.transform.position;
            navMeshAgent.isStopped = true;
        }
    }
}
