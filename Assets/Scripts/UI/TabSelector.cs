using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Transform tab;

    public void OnPointerClick(PointerEventData eventData)
    {
        //focus this tab when left clicking the selector.
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //moving a tab to the bottom of the hierarchy will render it on top.
            tab.SetAsLastSibling();
        }
    }
}
