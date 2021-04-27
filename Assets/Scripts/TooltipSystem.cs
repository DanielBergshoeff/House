using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//most code taken from Game Dev Guid at: https://www.youtube.com/watch?v=HXFoUGw7eKk
public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem current;
    public Tooltip tooltip;

    void Awake()
    {
        current = this;
    }

    public static void Show() {
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide() {
        current.tooltip.gameObject.SetActive(false);
    }

}

