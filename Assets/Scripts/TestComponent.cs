using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TestComponent : MonoBehaviour, IInteractable
    {
        public void Interact(PlayerController player)
        {
            Debug.Log("Hello World!");
        }
    }
}
