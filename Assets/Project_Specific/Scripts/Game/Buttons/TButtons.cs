using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class TButtons : MonoBehaviour
{
    [TitleGroup("Refs")] 
    [SerializeField] private GameObject m_Root;
    
    [Title("Animation")]
    [SerializeField] private AnimationParameters m_Animation;

    private Tween m_Tween;

    #region Init

    protected virtual void OnDisable()
    {
        m_Tween?.Kill();
    }

    #endregion
    
    #region Unity Mouse Function

    private void OnMouseDown()
    {
        onClick();
        punchOnClick();
    }

    #endregion

    #region Click

    protected virtual void onClick()
    {
       
    }

    #endregion

    #region Anim

    private void punchOnClick()
    {
        m_Root.transform.localScale = Vector3.one;

        m_Tween?.Kill();
        m_Tween = m_Root.transform.DOPunchScale(m_Animation.Punch.PunchVector, m_Animation.Punch.PunchDuration,
            m_Animation.Punch.PunchVibration, m_Animation.Punch.PunchElasticity);
    }

    #endregion

    #region Classes & Structs   

    [Serializable]
    private class AnimationParameters
    {
        public PunchTweenData Punch;
    }

    #endregion
}