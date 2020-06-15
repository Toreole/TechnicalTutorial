using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    //a simple script to test some physics.
    public class PhysicsTest : MonoBehaviour
    {
        [SerializeField] //this body should be kinematic!
        protected Rigidbody2D body;


        private void Start()
        {
            StartCoroutine(Move());
        }

        //a simple Coroutine to Move the kinematic body
        IEnumerator Move()
        {
            //this for loop executes on each frame for 5 seconds. t = time.
            for (float t = 0; t < 5; t += Time.deltaTime)
            {
                //move the body up, this applies its force and moves along all colliding rigidbody2d.
                body.MovePosition(body.position + Vector2.up * Time.deltaTime);
                yield return new WaitForFixedUpdate(); //!CORRECTION: wait for next physics update, instead of the next Frame.
            }
        }

    }
}