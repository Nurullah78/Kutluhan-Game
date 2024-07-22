using UnityEngine;

namespace Animations
{
    public class BoredBehaviour : StateMachineBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] float _timeUntilBored;
        [SerializeField] int _numberOfBoredAnimations;

        bool _isBored;
        float _idleTime;
        int _boredAnimation;


//---------------Metotlar---------------

        void ResetIdle()
        {
            if (_isBored && _boredAnimation > 0.01f)
            {
                _boredAnimation--;
            }
            
            _isBored = false;
            _idleTime = 0;
        }

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ResetIdle();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_isBored == false)
            {
                _idleTime += Time.deltaTime;
                
                if (_idleTime > _timeUntilBored && stateInfo.normalizedTime % 1 < 0.02f)
                {
                    _isBored = true;
                    _boredAnimation = Random.Range(1, _numberOfBoredAnimations + 1);
                    _boredAnimation = _boredAnimation * 2 - 1;
                    
                    animator.SetFloat("BoredAnimation", _boredAnimation - 1);
                }
            }
            else if (stateInfo.normalizedTime % 1 > 0.98f)
            {
                ResetIdle();
            }
            
            animator.SetFloat("BoredAnimation", _boredAnimation, 0.2f, Time.deltaTime);
        }
    }
}
