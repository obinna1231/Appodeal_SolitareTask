using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ItemVariableEditor
{
    [Title("Item")] 
    public TItem.Parameters Item;
    
    [Title("Modules")] 
    public ScaleItemModule.Parameters Scale;
    
    [Title("Data")]
    [SerializeField] private ItemDataDictionary m_CardData = new ItemDataDictionary();


    #region Gets

    public List<ItemData> GetAllCardData()
    {
        List<ItemData> allCards = new List<ItemData>();
        
        foreach (var data in m_CardData)
        {
            if (data.Value != null && data.Value.Cards != null)
            {
                allCards.AddRange(data.Value.Cards);
            }
        }
        return allCards;
    }

    public ItemData GetRandomData()
    {
        var allData = GetAllCardData();
        return allData[Random.Range(0, allData.Count - 1)];
    }

    #endregion
    
    #region Classes & Structs   

    [Serializable]
    private class ItemDataDictionary : UnitySerializedDictionary<eItemType, ItemListData> { }
    
    [Serializable]
    private class ItemListData
    {
        [SerializeField] private ItemData[] m_Cards;
        
        public ItemData[] Cards => m_Cards;
    }

    #endregion
    
}