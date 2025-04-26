using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public abstract class TCell : MonoBehaviour
{
    private Parameters m_Parameters => GameConfig.Instance.Gameplay.Field.Cells;

    [TitleGroup("Debug")] 
    [ShowInInspector, ReadOnly] protected List<TItem> Items = new List<TItem>();
    
    public bool IsEmpty => Items == null;

    #region Init

    private void OnEnable()
    {
        SaveManager.OnUndo += onUndo;
    }

    private void OnDisable()
    {
        SaveManager.OnUndo += onUndo;
    }

    #endregion

    #region Callback

    private void onUndo()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].transform.position = transform.position + ProjectUtils.GetItemOffset(i);
        }
    }

    #endregion
    
    #region Take / Free

    public virtual void TakeCell(TItem i_Item)
    {
        Items.Add(i_Item);
    }

    public virtual void Free(TItem i_Item)
    {
        if (Items.Contains(i_Item))
            Items.Remove(i_Item);
    }

    #endregion

    #region Gets

    public Vector3 GetMovePosition(TItem i_Item)
    {
        if(!Items.Contains(i_Item)) return Vector3.zero;

        var index = Items.IndexOf(i_Item);
        return transform.position + ProjectUtils.GetItemOffset(index);

    }

    public List<TItem> GetItemsBelow(TItem i_Item)
    {
        var index = Items.IndexOf(i_Item);
        return Items.Skip(index + 1).ToList();
    }

    #endregion

    #region Editor

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.forward, GameConfig.Instance.Gameplay.Field.Draggable.DragDistanceThreshold); 
#endif
    }

    #endregion

    #region Classes / Structs

    [Serializable]
    public class Parameters
    {
        public Vector3 PlacementOffset;
    }

    #endregion
}