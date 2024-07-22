using UnityEngine;

namespace Movement
{
    public class CharacterControllerGravity : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] float gravity = -9.81f;
        [SerializeField] float weight = 0.1f; // Ağırlık kat sayısı (farklı objelere göre ayarlanabilir)

        CharacterController characterController => GetComponent<CharacterController>();
        Vector3 velocity;


//---------------Metotlar---------------

        void Update()
        {
            if (characterController.enabled)
            {
                // Yerçekimi etkisi
                if (characterController.isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f; // Karakterin yere daha düzgün oturmasını sağlar
                }

                velocity.y += gravity * weight * Time.deltaTime;

                characterController.Move(velocity * Time.deltaTime);
            }
        }
    }
}
