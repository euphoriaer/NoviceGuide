using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClickMask:MonoBehaviour,ICanvasRaycastFilter   
    {
        
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return false;
        }
    }
}