using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;

[RequireComponent(typeof(Rigidbody))]
public class InteractBall : MonoBehaviour, IInteractable
{
    //the body used for the physics simulation
    public Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        if (body == null)
        {
            //just make sure we have the Rigidbody
            body = GetComponent<Rigidbody>();
        }
    }
    
    //Interact with the ball to make it jump
    public void Interact()
    {
        //add an impulse to the physics body.
        //impulse is defined by [ impulse = mass * velocity ], so the resulting speed of the ball is the impulse divided by the body's mass.
        //in this case the default mass of the body is 1, so the velocity will be (0, 5, 0) => 5 meters/second upwards.
        body.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
    }

    public void Interact(PlayerController player)
    {
        Interact();
    }
}
