using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public abstract class TItem : Draggable
{
    private Parameters m_Parameters => GameConfig.Instance.Gameplay.Field.Items.Item;
    
    #region Events

    public static event Action<TItem> OnSpawn = delegate(TItem i_Item) {  };
    public static event Action<TItem> OnChangeCell = delegate(TItem i_Item) {  };
    public static event Action<TItem> OnItemMoved = delegate(TItem i_Item) {  };

    #endregion

    
    [TitleGroup("Refs"),SerializeField] private GameObject m_Root;
    [TitleGroup("Refs"),SerializeField] private SpriteRenderer m_Image;


    [TitleGroup("Debug")]
    [ShowInInspector, ReadOnly] private ItemData m_ItemData;
    [ShowInInspector, ReadOnly] public TCell Cell { get; private set; }
    [ShowInInspector, ReadOnly] private List<TItem> m_DraggableCards;
    
    private IItemModule[] m_Modules;

    #region Init

    private void Awake()
    {
        setupModules();
        m_Modules.ForEach2(x => x.OnStartUp());
    }

    private void OnEnable()
    {
        setupModules();
        m_Modules.ForEach2(x => x.OnActivate());
    }

    private void OnDisable()
    {
        m_Modules.ForEach2(x => x.OnDeactivate());
    }

    #endregion
    
    #region Spawn

    public void Spawn(ItemData i_ItemData, TCell i_Cell, eItemSpawnType i_SpawnType = eItemSpawnType.ShowInstant)
    {
        Cell = i_Cell;
        m_ItemData = i_ItemData;
        
        updateVisual();

        if (i_Cell != null)
        {
            ChangeCell(i_Cell);
            
            MoveToCell();
        }

        switch (i_SpawnType)
        {
            case eItemSpawnType.ShowInstant:
                transform.localScale = Vector3.one;
                break;
            case eItemSpawnType.FromRandom:
                m_Root.transform.DOScale(Vector3.one, m_Parameters.RevealTween.Duration)
                .From(0f)
                .SetEase(m_Parameters.RevealTween.Ease)
                .SetDelay(m_Parameters.RevealTween.Delay);
                break;
           
        }
        
        OnSpawn?.Invoke(this);
    }

    #endregion

    #region Visual

    private void updateVisual()
    {
        m_Image.sprite = m_ItemData.Icon;
    }

    #endregion

    #region Change Cell

    public void ChangeCell(TCell i_Cell, bool i_IsFreePreviousCell = true)
    {
        if (i_Cell == null) return;
        
        if(i_IsFreePreviousCell) Cell?.Free(this);

        Cell = i_Cell;
        
        Cell.TakeCell(this);
        
        OnChangeCell?.Invoke(this);
    }

    #endregion
    
    #region Move

    public void MoveToCell(Action i_OnComplete = null, bool i_IsShowAnim = false)
    {
        var position = Cell.GetMovePosition(this);
        var distance = Vector3.Distance(position, transform.position);
        
        MoveToCell(distance * m_Parameters.MoveTween.Duration, m_Parameters.MoveTween.Ease, i_OnComplete, i_IsShowAnim);   
    }

    private void MoveToCell(float i_Duration, Ease i_Ease, Action i_OnComplete = null, bool i_IsShowAnim = false)
    {
        // if (i_IsShowAnim)
        // {
        //     m_Root.transform.DOScale(Vector3.one, m_Parameters.RevealTween.Duration)
        //         .From(0f)
        //         .SetEase(m_Parameters.RevealTween.Ease)
        //         .SetDelay(m_Parameters.RevealTween.Delay);
        // }
        // else
        // {
        //     transform.DOScale(Vector3.one, m_Parameters.ScaleTween.Duration)
        //         .SetEase(m_Parameters.ScaleTween.Ease)
        //         .SetDelay(m_Parameters.ScaleTween.Delay);
        // }
        
        moveToCell(i_Duration, i_Ease, i_OnComplete, i_IsShowAnim);
        
    }

    private void moveToCell(float i_Duration, Ease i_Ease, Action i_OnComplete = null, bool i_IsShowAnim = false)
    {
        var position = Cell.GetMovePosition(this);
        transform.DOMove(position, i_Duration)
            .SetEase(i_Ease)
            .OnComplete(() =>
            {
                i_OnComplete?.Invoke();
                OnItemMoved?.Invoke(this);
            });
    }

    #endregion

    #region Drag
    
    protected override void onDragStart()
    {
        base.onDragStart();
        Select();
        m_DraggableCards = Cell.GetItemsBelow(this);
        m_DraggableCards.ForEach(x => x.Select());
    }

    protected override void onDrag()
    {
        base.onDrag();
        if (m_State != eState.Drag) return;
        
        
        for (int i = 0; i < m_DraggableCards.Count; i++)
        {
            var card = m_DraggableCards[i];
            var targetPosition = transform.position + ProjectUtils.GetItemOffset((i + 1));

            card.transform.position = targetPosition;
        }
    }

    protected override void onDragEnd()
    {
        base.onDragEnd();

        dragEnd();
        Deselect();
        m_DraggableCards.ForEach(x => x.Deselect());
    }

    private void dragEnd()
    {
        var isNoAction = true;
        TCell targetCell = null;

        targetCell = validateCard();
        if (targetCell == null)
            targetCell = validateCell(transform.position, Field.Instance.Cells);
        
        
        if (targetCell != null)
        {
            isNoAction = false;

            var previousCell = Cell;
            ChangeCell(targetCell);
            MoveToCell();

            foreach (var card in m_DraggableCards)
            {
                card.ChangeCell(targetCell);
                card.MoveToCell();
            }

            m_DraggableCards.Insert(0, this);
            SaveManager.Instance.Record(m_DraggableCards, previousCell);
        }

        if (isNoAction)
        {
            MoveToCell();
            foreach (var card in m_DraggableCards)
            {
                card.MoveToCell();
            }
        }
    }

    #endregion

    #region Validate
    

    private TCell validateCell(Vector3 i_Position, TCell[] i_Cells)
    {
        float distance = float.MaxValue;
        TCell cell = null;
    
        for (int i = 0; i < i_Cells.Length; i++)
        {
            var dist = math.distance(i_Cells[i].transform.position, i_Position);
            if (dist < GameConfig.Instance.Gameplay.Field.Draggable.DragDistanceThreshold && dist <= distance && Cell != i_Cells[i])
            {
                distance = dist;
                cell = i_Cells[i];
            }
        }
    
        return cell;
    }

    private TCell validateCard()
    {
        Ray ray = new Ray(transform.position, Vector3.forward + new Vector3(0, 0, .2f));
        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 1000f, m_Parameters.ItemLayer))
        {
            var item = hit.collider.gameObject.GetComponent<TItem>();
            if (item != null && item.Cell != Cell)
                return item.Cell;
        }

        return null;
    }

    #endregion

    #region Modules

    private void setupModules()
    {
        m_Modules = GetComponentsInChildren<IItemModule>();
    }

    #endregion

    #region Select / Deselect

    public void Select()
    {
        m_Modules.ForEach2(x => x.OnSelect());
    }

    public void Deselect()
    {
        m_Modules.ForEach2(x => x.OnDeselect());
    }

    #endregion
    
    #region Classes & Structs

    [Serializable]
    public class Parameters
    {
        public LayerMask ItemLayer;
        public SimpleTweenData MoveTween;
        public SimpleTweenCurveData RevealTween;
    }

    #endregion
}