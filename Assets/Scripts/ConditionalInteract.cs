using System;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.ScriptableObjects;

namespace Tutorial
{
    public class ConditionalInteract : MonoBehaviour, IInteractable
    {
        //Specify the item you need to interact with this.
        [SerializeField]
        private Item condition;

        public void Interact(PlayerController player)
        {
            //Check whether the player has the required item.
            if(player.DoesPlayerHaveItem(condition))
            {
                //interaction is successful!
                Debug.Log("YAY");
            }
        }
    }
}
