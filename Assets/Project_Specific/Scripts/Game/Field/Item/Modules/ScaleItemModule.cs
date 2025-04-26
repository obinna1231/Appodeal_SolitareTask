using System;
using DG.Tweening;
using UnityEngine;

public class ScaleItemModule : TItemModule
{
    private Parameters m_Parameters => GameConfig.Instance.Gameplay.Field.Items.Scale;
    private Tween m_Tween;

    #region Init

    public override void OnDeactivate()
    {
        m_Tween?.Kill();
    }

    #endregion

    #region Select / Deselect

    public override void OnSelect()
    {
        m_Tween?.Kill();
        transform.DOScale(m_Parameters.Scale, m_Parameters.ScaleOut.Duration)
            .SetEase(m_Parameters.ScaleOut.Ease)
            .SetDelay(m_Parameters.ScaleOut.Delay);

    }

    public override void OnDeselect()
    {
        m_Tween?.Kill();
        transform.DOScale(Vector3.one, m_Parameters.ScaleIn.Duration)
            .SetEase(m_Parameters.ScaleIn.Ease)
            .SetDelay(m_Parameters.ScaleIn.Delay);
    }

    #endregion
    
    #region Classes / Structs

    [Serializable]
    public class Parameters
    {
        public Vector3 Scale = Vector3.one;
        public SimpleTweenData ScaleOut;
        public SimpleTweenData ScaleIn;
    }

    #endregion
}