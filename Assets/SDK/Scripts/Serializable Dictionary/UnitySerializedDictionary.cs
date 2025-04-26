using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    protected List<TKey> m_KeyData = new List<TKey>();

    [SerializeField, HideInInspector]
    protected List<TValue> m_ValueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();

        for (int i = 0; i < this.m_KeyData.Count; i++)
        {
            this[m_KeyData[i]] = m_ValueData[i];
        }
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        m_KeyData.Clear();
        m_ValueData.Clear();

        foreach (var item in this)
        {
            m_KeyData.Add(item.Key);
            m_ValueData.Add(item.Value);
        }

    }
}
