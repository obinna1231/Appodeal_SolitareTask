using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using WayneSDK;
using Random = UnityEngine.Random;

public class Field : Singleton<Field>
{
    [Title("Refs")] 
    [SerializeField] private TCell[] m_Cells;
    
    public TCell[] Cells => m_Cells.ToArray();

    public TCell RandomCell => Cells[Random.Range(0, m_Cells.Length)];
    
    public TCell FirstEmptyCell => Cells.FirstOrDefault(x => x.IsEmpty);
    
    public bool IsEmptyCells => FirstEmptyCell != null;

    #region Init

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    #endregion
    
}