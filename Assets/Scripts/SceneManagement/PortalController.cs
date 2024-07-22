using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Movement;
using GameController;

namespace SceneManagement
{
    public class PortalController : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
         int sceneToLoad;
        [SerializeField]
         float fadeWaitTime, fadeOutTime, fadeInTime;

        //public Portal[] Portal;

        public GameObject[] _player => GameObject.FindGameObjectsWithTag("Player");
        public GameObject[] _core => GameObject.FindGameObjectsWithTag("Core");
        public GameObject[] _controllers => GameObject.FindGameObjectsWithTag("Controllers");

        Vector3 playerBasePosition;

        Fader _fader;

        int playerID;


//---------------Metotlar---------------

        void CreatingObjects()
        {
            foreach (var player in _player)
            {
                if (_player.Length > 1 && player.transform.position == playerBasePosition)
                    Destroy(player);

                DontDestroyOnLoad(player);
                player.name = "Ninja";
                _player[0] = player;

                foreach (var core in _core)
                {
                    if (core.GetComponent<PersistentObjectSpawner>().player.name != player.name)
                        Destroy(core);

                    DontDestroyOnLoad(core);
                    _core[0] = core;
                }

                foreach (var controllers in _controllers)
                {
                    if (controllers.GetComponent<PlayerFinder>().player.name != player.name)
                        Destroy(controllers);

                    DontDestroyOnLoad(controllers);
                    _controllers[0] = controllers;
                }
            }

            if (_fader == null)
                _fader = FindObjectOfType<Fader>();
        }

        void Start()
        {
            playerBasePosition = new Vector3(30.65f, 0, -19.8f);
            CreatingObjects();
        }

        void LoadScene()
        {
            _player[playerID].GetComponent<NavMeshAgent>().enabled = false;
            _player[playerID].GetComponent<CharacterController>().enabled = false;
            _player[playerID].GetComponent<CharacterControllerGravity>().enabled = false;

            SceneManager.LoadScene(sceneToLoad);
        }

        IEnumerator UpdatePlayer(Portal portal)
        {

            while (_player[playerID].transform.position != portal.spawnPoint.position)
            {
                _player[playerID].transform.position = portal.transform.GetChild(0).position;
                _player[playerID].transform.rotation = portal.transform.GetChild(0).rotation;
            }

            yield return null;
        }

        void OpenObjects()
        {
            _player[playerID].GetComponent<NavMeshAgent>().enabled = true;
            _player[playerID].GetComponent<CharacterController>().enabled = true;
            _player[playerID].GetComponent<CharacterControllerGravity>().enabled = true;
        }

        public IEnumerator Transition(Portal callingPortal)
        {
            DontDestroyOnLoad(gameObject);

            yield return _fader.FadeOut(fadeOutTime);

            LoadScene();

            yield return new WaitForSeconds(fadeWaitTime / 3);

            Portal portal = callingPortal.GetOtherPortal();

            yield return new WaitForSeconds(fadeWaitTime / 3);

            StartCoroutine(UpdatePlayer(portal));

            yield return new WaitForSeconds(fadeWaitTime / 3);
            OpenObjects();

            yield return _fader.FadeIn(fadeInTime);
            

            yield return new WaitForSeconds(fadeWaitTime / 3);
            
            Destroy(gameObject);
        }
    }
}
