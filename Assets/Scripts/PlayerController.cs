using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //the speed in meters per second that the player moves with.
    public float speed = 4f;

    //the input axis for sideways movement
    public string sidewaysAxis = "Horizontal";
    //the input axis for forward movement
    public string forwardAxis = "Vertical";

    //the character controller we use to move the player.
    public CharacterController character;

    //the angles per second we rotate our player and camera with
    public float rotationSpeed = 180f;
    //the rotation axis around the Y axis of the player
    public string yRotationAxis = "Mouse X";
    //the rotation axis around the X axis of the camera.
    public string xRotationAxis = "Mouse Y";

    //the minimum rotation of the camera on X
    public float minRotX = -70;
    //the maximum rotation of the camera on X
    public float maxRotX = 80;
    
    //the camera that represents the players head.
    public new Transform camera;

    //the current rotation of the camera on the X axis.
    private float currentRotX = 0f;

    //the layermask that we want to use for detection.
    public LayerMask detectionMask;

    //the button we use to interact with objects.
    public string interactButton = "Interact";

    //initial setup
    private void Start()
    {
        if(character == null) //null => is not assigned
        {
            //get the attached CharacterController component (always exists because of the [RequireComponent] attribute at the top!)
            character = GetComponent<CharacterController>();
        }
        if(camera == null) //null => is not assigned
        {
            if (transform.childCount == 0) //there is no child (camera) on this player!
            {
                //this creates an empty gameobject with the name Camera.
                GameObject cam = new GameObject("Camera");
                cam.transform.parent = this.transform;
                //reset the new camera's local position to be in the center of the player.
                cam.transform.localPosition = Vector3.zero;
                //Add the Camera component so we can actually see things!
                cam.AddComponent<Camera>();
                //Add the AudioListener so we can hear sounds!
                cam.AddComponent<AudioListener>();

                camera = cam.transform; //assign it!
                return; //stop this method call.
            }
            else //there is actually a child, just get the first one.
            {
                camera = transform.GetChild(0);
            }
        }
    }

    //Update our rotation, movement and detection every frame of the game!
    void Update()
    {
        Rotation();
        Move();
        DetectObject();
    }

    //Move the character by the WASD input
    void Move()
    {
        //Get the Input
        //! Alternatively Input.GetAxisRaw can be used, since Input.GetAxis smoothes out the values between 0 and 1,
        //! ^this makes Input.GetAxisRaw feel much snappier!
        float xMovement = Input.GetAxis(axisName: sidewaysAxis);
        float zMovement = Input.GetAxis(axisName: forwardAxis);

        //combine your movement direction based on the direction our player is facing.
        Vector3 movement = transform.forward * zMovement + transform.right * xMovement;
        //Debug.Log(movement.magnitude); <- can vary a lot
        movement.Normalize(); //<- important because it resets the Vector's Length to 1, avoids the player moving faster when he's going diagonally.
        //Debug.Log(movement.magnitude); <- is always 1 or 0.999999 (essentially 1)
        
        //multiply our movement direction with the speed to get the current directional velocity.
        //the same as movement = movement * speed;
        movement *= speed;

        //SimpleMove applies gravity and the CharacterController handles collision for us!
        //It expects a velocity, so there is no need to multiply this with Time.deltaTime!
        character.SimpleMove(movement);
        //transform.Translate(movement * Time.deltaTime); <- dont do this please
    }

    //Rotate the player along the specified axes of the mouse.
    void Rotation()
    {
        //mouse X movement
        float yRotation = Input.GetAxis(axisName: yRotationAxis);
        yRotation *= rotationSpeed * Time.deltaTime;

        //mouse Y movement
        float xRotation = Input.GetAxis(axisName: xRotationAxis);
        xRotation *= rotationSpeed * Time.deltaTime;

        //new currentRotation on the X axis of the camera.
        currentRotX = currentRotX + xRotation;
        //limit the rotation on the X axis so we dont break our necks.
        currentRotX = Mathf.Clamp(currentRotX, minRotX, maxRotX);
        
        //rotate the player around the y axis to look around.
        transform.Rotate(0, yRotation, 0);
        //camera.Rotate(xRotation, 0, 0);
        //directly set the camera's up/down (x rotation) the the limited value we have.
        camera.localEulerAngles = new Vector3(currentRotX, 0, 0);
    }

    //detects an object in front of the camera.
    void DetectObject()
    {
        //try to find a collider using a Raycast
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxDistance: 2f, detectionMask))
        {
            //Check if the Player is pressing the Interact button specified in the Project Settings.
            if (Input.GetButtonDown(buttonName: interactButton)) 
            {
                //Draw a line that's only visible in the Scene View of the editor to help visualize what we did!
                Debug.DrawLine(camera.position, hit.transform.position, Color.red, 10000);

                //try to find a InteractBall script
                InteractBall ball = hit.transform.GetComponent<InteractBall>();

                if (ball) //if the ball exists
                {
                    //Interact with the ball!
                    ball.Interact();
                }
            }
        }
    }

}