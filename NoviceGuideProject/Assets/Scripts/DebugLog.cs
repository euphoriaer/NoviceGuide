using UnityEngine;

namespace DefaultNamespace
{
    public class DebugLog:MonoBehaviour
    {
        public void Log()
        {
            Debug.Log("点击了: "+this.gameObject.name);
        }
    }
}