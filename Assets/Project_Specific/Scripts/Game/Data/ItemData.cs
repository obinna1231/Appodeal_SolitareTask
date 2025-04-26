using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class ItemData
{
    [HorizontalGroup("Item", Width = 200)]
    
    [SerializeField,VerticalGroup("Item/Info"), LabelWidth(60)] private eItemType m_Type;
    [SerializeField,VerticalGroup("Item/Info"), LabelWidth(60)] private eItemNumber m_Number;
    
    [BoxGroup("Item/Icon", CenterLabel = true)]
    [PreviewField(120, ObjectFieldAlignment.Center), HideLabel, TableColumnWidth(60),SerializeField] private Sprite m_Icon;
    
    
    public eItemType Type => m_Type;
    public eItemNumber Number => m_Number;
    public Sprite Icon => m_Icon;
}