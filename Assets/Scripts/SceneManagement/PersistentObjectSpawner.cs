using UnityEngine;

namespace SceneManagement
{
    public class PersistentObjectSpawner : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
         GameObject persistentGameObjectPrefab;

        public GameObject persistentObject;

        public GameObject player;

        static bool hasSpawned;
        

//---------------Metotlar---------------

        void SpawnPersistentObject()
        {
            persistentObject = Instantiate(persistentGameObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

        void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObject();
            hasSpawned = true;
        }
    }
}
