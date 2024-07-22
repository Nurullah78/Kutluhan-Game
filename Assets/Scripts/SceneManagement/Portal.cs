using UnityEngine;

namespace SceneManagement
{
    public class Portal : MonoBehaviour
    {

//---------------Nesneler---------------

        enum DestinationIdentifier
        {
            İnsanGeçidi,
            B,
            C,
            D,
            E
        }

        [SerializeField]
         DestinationIdentifier destination;

        public PortalController _portalController;
        public Transform spawnPoint;


//---------------Metotlar---------------

        public Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(_portalController.Transition(this));
            }
        }
    }
}
