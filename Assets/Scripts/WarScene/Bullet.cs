using UnityEngine;
using Core;

namespace WarScene
{
    public class Bullet : MonoBehaviour
    {

//----------------- Nesneler -----------------

        [SerializeField]
         float speed = 1f;

        CharacterState target = null;

        float damage = 0;


//----------------- Metotlar -----------------

        public void SetTarget(CharacterState target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();

            if (targetCollider == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCollider.height / 1.4f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterState>() != target) return;

            if (target.CompareTag("Player"))
                target.PlayerTakeDamage(damage);
            else
                target.EnemyTakeDamage(damage);

            speed = 0;

            Destroy(gameObject, 3);
        }

        void Update()
        {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
