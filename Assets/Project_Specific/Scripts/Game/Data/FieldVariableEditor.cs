using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class FieldVariableEditor
{
    [Title("Elements")]
    public Draggable.Parameters Draggable;
    public TCell.Parameters Cells;
    public ItemVariableEditor Items;
}