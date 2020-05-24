using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeCorner : MonoBehaviour, IDragHandler
{
    [SerializeField] //the rect to resize.
    private RectTransform target;

    //dragging should do it
    public void OnDrag(PointerEventData eventData)
    {
        //start by getting the current size of the rect.
        Vector2 currentSize = target.rect.size;
        //get the delta (movement of the cursor)
        Vector2 delta = eventData.delta;

        //adjust the position by half delta to correctly resize our rect.
        Vector2 currentPosition = target.anchoredPosition;
        currentPosition += delta / 2f;
        target.anchoredPosition = currentPosition;

        //add the size. needs negative delta.y because our ui element grows towards the bottom of the screen.
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize.x + delta.x);
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize.y - delta.y);
    }
}
