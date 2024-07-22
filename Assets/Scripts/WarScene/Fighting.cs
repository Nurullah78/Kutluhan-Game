using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Animations;
using Movement;

namespace WarScene
{
    public class Fighting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

//---------------Nesneler---------------

        GameObject Player => GameObject.FindWithTag("Player");

        bool isFisting, isFighting;


//---------------Metotlar---------------

        IEnumerator StartFist()
        {
            Player.GetComponent<AnimationController>().AnimSetIsFighting(true);
            isFighting = true;
            isFisting = true;
            Player.GetComponent<AnimationController>().AnimSetFisting(true);

            yield return null;
        }

        IEnumerator EndFist()
        {
            isFisting = false;
            isFighting = false;
            Player.GetComponent<AnimationController>().AnimSetFisting(false);
            yield return new WaitForSeconds(5);

            Player.GetComponent<AnimationController>().AnimSetIsFighting(false);
            yield return null;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(StartFist());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(EndFist());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(StartFist());


            if (isFighting)
                if (isFisting)
                    Player.GetComponent<Fighter>().AttackMethod();

            if (Player != null && Player.GetComponent<MoveCharacter>().movementDirection != Vector3.zero)
                Player.GetComponent<AnimationController>().AnimSetIsFighting(false);


            if (Input.GetKeyUp(KeyCode.E))
                StartCoroutine(EndFist());
        }
    }
}
