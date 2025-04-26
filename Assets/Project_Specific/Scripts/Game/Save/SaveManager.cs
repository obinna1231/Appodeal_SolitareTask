using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using WayneSDK;

public class SaveManager : Singleton<SaveManager>
{

    #region Events

    public static event Action OnRecord = delegate { };
    public static event Action OnUndo = delegate { };

    #endregion
    
    private Stack<SaveData> m_Save = new Stack<SaveData>();
    [ShowInInspector,ReadOnly] public int AvailableMove => m_Save.Count;

    #region Init

    protected override void OnEnable()
    {
        base.OnEnable();
        UndoButton.OnUndo += onUndo;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UndoButton.OnUndo -= onUndo;
    }

    #endregion

    #region Callback

    private void onUndo()
    {
        undo();
    }

    #endregion
    
    #region Record

    public void Record(List<TItem> i_Items, TCell i_Cell)
    {
        m_Save.Push(SaveData.Create(i_Items, i_Cell));
        OnRecord?.Invoke();
    }

    #endregion

    #region Undo

    
    private void undo()
    {
        if (m_Save.Count == 0) return;
        var save = m_Save.Pop();

        foreach (var item in save.Items)
        {
            item.ChangeCell(save.Cell);
            item.MoveToCell();
        }
        OnRecord?.Invoke();
    }

    #endregion
    
}

[Serializable]
public class SaveData
{
    public List<TItem> Items;
    public TCell Cell;

    public static SaveData Create(List<TItem> i_Item, TCell i_Cell)
    {
        return new SaveData()
        {
            Items = i_Item,
            Cell = i_Cell
        };
    }
}
