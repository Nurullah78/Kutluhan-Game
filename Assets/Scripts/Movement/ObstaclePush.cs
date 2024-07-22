using UnityEngine;

namespace Movement
{
    public class ObstaclePush : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] float forceMagnitude;
        

//---------------Metotlar---------------

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody rigidbody = hit.collider.attachedRigidbody;

            if (rigidbody != null && !hit.gameObject.CompareTag("Enemy"))
            {
                Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();
                
                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
            }
        }
    }
}
