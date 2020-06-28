using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public static class Util
    {
        public static void Hello(this UnityEngine.Object obj)
        {
            Debug.Log($"Hello, I am {obj.name}");
        }

        public static void Jump(this Rigidbody body, float jumpHeight)
        {
            body.AddForce(new Vector3(0, jumpHeight), ForceMode.Impulse);
        }
    }
}