using System;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.ScriptableObjects;

namespace Tutorial
{
    public class CollectItem : MonoBehaviour, IInteractable
    {
        //Specify the item to collect from this object.
        [SerializeField]
        private Item itemToCollect;

        public void Interact(PlayerController player)
        {
            //Add the item to the player's inventory
            player.AddItemToPlayer(itemToCollect);
            //disable the object to prevent the player from getting duplicates of this object!
            gameObject.SetActive(false);
        }
    }
}
