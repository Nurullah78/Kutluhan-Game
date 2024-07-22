using UnityEngine;

namespace StatusBar
{
    public class CanvasTowardController : MonoBehaviour
    {
        Camera MainCamera => Camera.main;
        
        void Update()
        {
            transform.LookAt(transform.position + MainCamera.transform.rotation * Vector3.back, MainCamera.transform.rotation * Vector3.up);
        }
    }
}
