using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Draggable : MonoBehaviour
{

    private Parameters m_Parameters => GameConfig.Instance.Gameplay.Field.Draggable;
    
    [TitleGroup("Refs")]
    [SerializeField] private BoxCollider m_Collider;


    [TitleGroup("Debug")] 
    [ShowInInspector, ReadOnly] protected eState m_State;


    private Vector3 m_InitialDragPosition;
    private Vector3 m_InitialHitPoint;
    private Vector3 m_DragPosition;

    #region Unity Loop

    private void Update()
    {
        onDrag();
    }

    #endregion

    #region Unity Mouse Functions

    private void OnMouseDown()
    {
        if (m_State == eState.Idle)
        {
            onDragStart();
        }
    }
    
    
    private void OnMouseUp()
    {
        if (m_State == eState.Drag)
        {
            onDragEnd();
        }
    }

    #endregion
   
    #region Drag Functions


    protected virtual void onDragStart()
    {
        setState(eState.Drag);
        
        m_InitialDragPosition = transform.position;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, m_Parameters.Layer))
        {
            m_InitialHitPoint = hit.point;
        }

        m_DragPosition = transform.position + m_Parameters.DragOffset;
    }

    protected virtual void onDrag()
    {
        if (m_State != eState.Drag) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, m_Parameters.Layer))
        {
            Vector3 delta = hit.point - m_InitialHitPoint;
            m_DragPosition = m_InitialDragPosition + delta + m_Parameters.DragOffset;
        }
        
        transform.position = Vector3.Lerp(transform.position, m_DragPosition, m_Parameters.LerpSpeed * Time.deltaTime);
    }


    protected virtual void onDragEnd()
    {
        setState(eState.Idle);
    }

    #endregion

    #region State

    protected void setState(eState i_State)
    {
        m_State = i_State;

        m_Collider.enabled = m_State != eState.Drag;
    }

    #endregion
    
    #region Classes / Structs / Enums

    protected enum eState
    {
        Idle,
        Drag
    }
    
    [Serializable]
    public class Parameters
    {
        public float LerpSpeed;
        public Vector3 DragOffset;
        public LayerMask Layer;
        public float DragDistanceThreshold;
    }

    #endregion
}