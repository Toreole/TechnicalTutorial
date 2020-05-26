using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizeCorner : MonoBehaviour, IDragHandler
{
    [SerializeField] //the rect to resize.
    private RectTransform target;
    [SerializeField]
    private CanvasScaler context;
    [SerializeField]
    private bool isCentralPivot; //for 0.5/0.5 pivot tick: true, for 0/1 pivot: false

    Display display;

    private void Start()
    {
        display = Display.main;
    }

    //dragging should do it
    public void OnDrag(PointerEventData eventData)
    {
        //start by getting the current size of the rect.
        Vector2 currentSize = target.rect.size;
        //get the delta (movement of the cursor)
        Vector2 delta = eventData.delta;

        //scale delta relative to the display context. (not that this only "correctly" scales with the same aspect ratio)
        Vector2 reference = context.referenceResolution;
        delta.x *= (reference.x / display.renderingWidth);
        delta.y *= (reference.y / display.renderingHeight);

        //adjust the position by half delta to correctly resize our rect.
        //!Note: this way of offsetting the position is only required when using a central pivot! (0.5, 0.5)
        if (isCentralPivot)
        {
            Vector2 currentPosition = target.anchoredPosition;
            currentPosition += delta / 2f;
            target.anchoredPosition = currentPosition;
        }

        //add the size. needs negative delta.y because our ui element grows towards the bottom of the screen.
        //print(target.sizeDelta.ToString());
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize.x + delta.x);
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize.y - delta.y);
    }
}
