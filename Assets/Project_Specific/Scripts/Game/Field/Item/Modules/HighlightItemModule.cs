using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class HighlightItemModule : TItemModule
{
    [Title("Refs")]
    [SerializeField] private GameObject m_Root;

    #region Init

    public override void OnActivate()
    {
        m_Root.SetActive(false);
    }
    
    #endregion

    #region Select / Deselect

    public override void OnSelect()
    {
        m_Root.SetActive(true);
    }

    public override void OnDeselect()
    {
        m_Root.SetActive(false);
    }

    #endregion
}