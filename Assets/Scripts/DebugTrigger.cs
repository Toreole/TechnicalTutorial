using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    [AddComponentMenu("TriggerComponent/Debug"), RequireComponent(typeof(BoxCollider))]
    public class DebugTrigger : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent enterTriggerCallback;

        [SerializeField] //make the tag we check for serialized, in case we want to check for another one, or do a typo!
        private string checkTag = "Player";

        //called in the editor when making changes to the DebugTrigger component!
        private void OnValidate()
        {
            //just make sure that our BoxCollider is a trigger.
            GetComponent<BoxCollider>().isTrigger = true;
        }

        //this is called when a Collider with a Rigidbody / a CharacterController enters the volume of the trigger collider.
        private void OnTriggerEnter(Collider other)
        {
            //use CompareTag instead of == to check tags, it's much more reliable!
            if (other.CompareTag(checkTag))
                enterTriggerCallback.Invoke();
        }

        //a simple public method to test the UnityEvent.
        public void TestThis()
        {
            Debug.Log("some message");
        }
    }
}