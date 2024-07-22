using UnityEngine;

namespace Animations
{
    public class AnimationController : MonoBehaviour
    {

//---------------Nesneler---------------

        Animator Animator => GetComponent<Animator>();


//---------------Metotlar---------------

        public void AnimSetInputMagnitude(float value)
        {
            Animator.SetFloat("InputMagnitude", value, 0.05f, Time.deltaTime);
        }

        public void AnimSetIsMoving(bool value)
        {
            Animator.SetBool("IsMoving", value);
        }

        public void AnimSetDieTrigger()
        {
            Animator.SetTrigger("Die");
        }

        public void AnimSetIsFighting(bool value)
        {
            Animator.SetBool("IsFighting", value);
        }

        public void AnimSetFisting(bool value)
        {
            Animator.SetBool("Fisting", value);
        }
    }
}
