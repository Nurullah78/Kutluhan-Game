using UnityEngine;

namespace TouchControl
{
    public class TouchController : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] FixedTouchField _FixedTouchField;
        [SerializeField] CameraLook _CameraLook;
        

//---------------Metotlar---------------

        void Update()
        {
            _CameraLook.LockAxis = _FixedTouchField.TouchDist;
        }
    }
}
