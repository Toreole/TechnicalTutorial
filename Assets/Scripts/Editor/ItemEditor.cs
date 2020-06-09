using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.ScriptableObjects;
using UnityEditor;

namespace Tutorial.Editing
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        //just "copy paste" properties for all the variables in our Item
        SerializedProperty nameProperty;
        SerializedProperty typeProperty;
        SerializedProperty damageProperty;

        private void OnEnable()
        {
            //find the properties of the serialized object (our asset) via their variable names.
            //use nameof instead of typing out the string! When you rename the variable it would break otherwise.
            nameProperty = serializedObject.FindProperty(nameof(Item.itemName)); // "itemName"
            typeProperty = serializedObject.FindProperty(nameof(Item.type)); // "type"
            damageProperty = serializedObject.FindProperty(nameof(Item.weaponDamage)); // "weaponDamage"
        }

        public override void OnInspectorGUI()
        {
            //the actual object we are inspecting (the runtime representation)
            //See class Item for info.
            Item item = (Item)target;

            //actually change the properties we have.
            EditorGUILayout.PropertyField(nameProperty);
            EditorGUILayout.PropertyField(typeProperty);

            //check for the item type, inspect different things depending on it.
            if (item.type == Item.ItemType.KeyItem)
            {
                EditorGUILayout.LabelField("This is a key item!");
            }
            else if(item.type == Item.ItemType.Weapon)
            {
                //weapon only cares about its damage, so only show that.
                EditorGUILayout.PropertyField(damageProperty);
            }
            else if(item.type == Item.ItemType.Potion)
            {
                EditorGUILayout.LabelField("This is a potion!");
            }

            //apply the modified properties to actually save the changed data, and update our inspector.
            serializedObject.ApplyModifiedProperties();
        }
    }
}