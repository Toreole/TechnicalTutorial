using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.ScriptableObjects
{
    //Define a creatable Asset, available by rightclicking in the project tab (folder) Create -> Item
    [CreateAssetMenu(fileName = "new Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        //The item only has a name, not other data yet.
        public string itemName;
    }
}
