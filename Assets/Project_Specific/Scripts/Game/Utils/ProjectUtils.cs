using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class ProjectUtils
{
    #region Linq

    public static void Shuffle_Wayne<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
    
    public static void ForEach2<T>(this T[] i_Array, Action<T> i_Action)
    {
        foreach (var x in i_Array)
        {
            i_Action.InvokeSafe(x);
        }
    }

    #endregion

    #region Actions

    public static void InvokeSafe<T>(this Action<T> i_Action, T i_Value)
    {
        if (i_Action != null)
        {
            i_Action.Invoke(i_Value);
        }
    }


    #endregion

    #region UI

    public static GameObject IsOverlapping(RectTransform i_RectTransform, GraphicRaycaster i_Raycaster, EventSystem i_EventSystem)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, i_RectTransform.position);
        
        PointerEventData pointerEventData = new PointerEventData(i_EventSystem)
        {
            position = screenPoint
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        i_Raycaster.Raycast(pointerEventData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == i_RectTransform.gameObject)
                continue; 
            
            return result.gameObject;
        }

        return null; 
    }

    #endregion

    #region Gameplay

    public static Vector3 GetItemOffset(int i_Index)
    {
        return new Vector3(0, 
            GameConfig.Instance.Gameplay.Field.Cells.PlacementOffset.y * i_Index,
            GameConfig.Instance.Gameplay.Field.Cells.PlacementOffset.z * i_Index);
    }

    #endregion
}