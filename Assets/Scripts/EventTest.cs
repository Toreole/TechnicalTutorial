using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tutorial
{
    public class EventTest : MonoBehaviour
    {
        /**
         *  event SomeEvent = {
         *          HelloWorld();
         *          SomeMethod();
         *  }
         */
         
        private void OnEnable()
        {
            CustomEvent<int>.AddListener(SomeMethod);
        }

        private void OnDisable()
        {
            CustomEvent<int>.RemoveListener(SomeMethod);
        }

        private void HelloWorld(string message)
        {
            Debug.Log(message);
        }

        void SomeMethod(int x)
        {
            Debug.Log(x);
        }

        public void InvokeIntEvent()
        {
            CustomEvent<int>.Invoke(5);
        }
    }
}