using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UndoButton : TButtons
{
    #region Events

    public static event Action OnUndo = delegate { };

    #endregion
    
    [TitleGroup("Refs")] 
    [SerializeField] private TextMeshPro m_CountText;

    #region Init

    private void OnEnable()
    {
        SaveManager.OnRecord += onRecord;
        SaveManager.OnUndo += onUndo;
    }

    private void Start()
    {
        updateCount();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SaveManager.OnRecord -= onRecord;
        SaveManager.OnUndo -= onUndo;
    }

    #endregion

    #region Callback

    private void onRecord()
    {
        updateCount();
    }

    private void onUndo()
    {
        updateCount();
    }

    #endregion

    #region Visual

    private void updateCount()
    {
        m_CountText.SetText($"{SaveManager.Instance.AvailableMove}");
    }

    #endregion

    #region Click

    protected override void onClick()
    {
        OnUndo?.Invoke();
    }

    #endregion
}