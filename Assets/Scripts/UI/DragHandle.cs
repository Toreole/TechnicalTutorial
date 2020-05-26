using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tutorial.UI
{
    public class DragHandle : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] //the target we want to drag around.
        private Transform target;

        //the offset from the cursor
        private Vector2 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            //upon starting to drag around the window we need to get the offset.
            offset = (Vector2)target.position - eventData.position;
            target.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //adjust our position to follow the drag.
            target.position = eventData.position + offset;
        }
    }
}