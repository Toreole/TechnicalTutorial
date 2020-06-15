using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    //Just an empty and pretty useless component to test our interface.
    public class TestComponent : MonoBehaviour, IInteractable
    {
        //the method of the interface that we have to use.
        public void Interact(PlayerController player)
        {
            Debug.Log("Hello World!");
        }
    }
}
