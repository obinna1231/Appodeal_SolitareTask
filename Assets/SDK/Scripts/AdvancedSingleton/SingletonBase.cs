using UnityEngine;

namespace WayneSDK
{

    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected virtual void Awake()
        {

        }
    }
}
