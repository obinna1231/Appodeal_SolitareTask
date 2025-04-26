using Sirenix.OdinInspector;
using UnityEngine;
using WayneSDK;

public class ItemManager : Singleton<ItemManager>
{
    [Title("Refs")] 
    [SerializeField] private Transform m_ItemsParent;

    #region Init

    protected override void OnEnable()
    {
        base.OnEnable();
        SpawnButton.OnSpawnItem += onSpawn;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SpawnButton.OnSpawnItem -= onSpawn;
    }

    #endregion

    #region Callback

    private void onSpawn(Vector3 i_Position)
    {
        var randomData = GameConfig.Instance.Gameplay.Field.Items.GetRandomData();
        if(randomData != null)
            spawnItem(randomData, i_Position);
    }

    #endregion
    
    #region Spawn

    private void spawnItem(ItemData i_ItemData, Vector3 i_SpawnPos)
    {
        var item = Instantiate(GameConfig.Instance.Gameplay.Prefabs.Item, m_ItemsParent.transform);
        TCell targetCell = null;
        if (Field.Instance.IsEmptyCells)
        {
            targetCell = Field.Instance.FirstEmptyCell;
        }
        else
        {
            targetCell = Field.Instance.RandomCell;
        }

        item.transform.position = i_SpawnPos;
        item.Spawn(i_ItemData, targetCell, eItemSpawnType.FromRandom);

    }

    #endregion
}