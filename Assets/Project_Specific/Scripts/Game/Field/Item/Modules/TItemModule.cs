using System;
using UnityEngine;

public abstract class TItemModule : MonoBehaviour, IItemModule
{
    #region Select / Deselect

    public virtual void OnStartUp() { }

    public virtual void OnActivate() { }

    public virtual void OnDeactivate() { }

    public virtual void OnSelect() { }

    public virtual void OnDeselect() { }

    #endregion
}