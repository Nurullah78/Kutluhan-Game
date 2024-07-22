using System;
using UnityEngine;

namespace TouchControl
{
    public class CameraLook : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField] float XSensivity = 35f, YSensivity = 15f, YRotation;
        [NonSerialized] public Vector2 LockAxis;
        
        float XMove, YMove, XRotation = 32;
        

//---------------Metotlar---------------

        void Update()
        {
            XMove = LockAxis.x * XSensivity * Time.deltaTime;
            YMove = LockAxis.y * YSensivity * Time.deltaTime;
            
            XRotation -= YMove;
            XRotation = Mathf.Clamp(XRotation, -10f, 80f);

            YRotation += XMove;

            transform.localRotation = Quaternion.Euler(XRotation, YRotation, 0);
        }
    }
}
