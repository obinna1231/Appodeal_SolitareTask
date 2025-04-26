using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WayneSDK
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T m_Instance = null;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    T[] objs = Resources.FindObjectsOfTypeAll<T>();

                    if(objs == null || objs.Length == 0)
                    {
                        //failed, so lets try "Resournces.LoadAll"
                        objs = Resources.LoadAll<T>(string.Empty);
                    }

                    if(objs.Length > 0 && objs[0] != null)
                    {
                        m_Instance = objs[0];
                    }

                    if(objs.Length > 1)
                    {
                        Debug.LogError("SingletonScriptableObject Error, You have more than 1 Object of the same type" + typeof(T).FullName);
                    }
                    if(objs.Length == 0)
                    {
                        Debug.LogError("SingletonScriptableObject Error, Can't find the object" + typeof(T).FullName + "Make sure its in the resources folder");
                    }
                }

                return m_Instance;
            }
        }
    }
}
