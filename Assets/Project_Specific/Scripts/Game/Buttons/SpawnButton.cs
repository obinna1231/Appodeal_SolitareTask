using System;
using UnityEngine;

public class SpawnButton : TButtons
{
    #region Event

    public static event Action<Vector3> OnSpawnItem = delegate(Vector3 i_Position) { };

    #endregion

    #region Click

    protected override void onClick()
    {
        OnSpawnItem?.Invoke(transform.position);
    }

    #endregion
}