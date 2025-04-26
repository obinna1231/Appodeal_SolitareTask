using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class SimpleTweenData
{
    public float Duration;
    public Ease Ease;
    public float Delay = 0;
}

[Serializable]
public class SimpleTweenCurveData
{
    public float Duration;
    public AnimationCurve Ease;
    public float Delay = 0;
}

[Serializable]
public class SimpleTweenVectorData
{
    public Vector3 Power;
    public float Duration;
    public Ease Ease;
    public float Delay = 0;
}

[Serializable]
public class PunchTweenData
{
    public Vector3 PunchVector;
    public float PunchDuration;
    public int PunchVibration = 10;
    public float PunchElasticity = 1;
    public Ease PunchEase;
}

