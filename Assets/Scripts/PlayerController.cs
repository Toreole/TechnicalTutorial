using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial.ScriptableObjects;
using Tutorial;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //the speed in meters per second that the player moves with.
    [SerializeField]
    float speed = 4f;

    //the input axis for sideways movement
    [SerializeField]
    private string sidewaysAxis = "Horizontal";
    //the input axis for forward movement
    [SerializeField]
    private string forwardAxis = "Vertical";

    //the character controller we use to move the player.
    [SerializeField]
    private CharacterController character;

    //the angles per second we rotate our player and camera with
    [SerializeField]
    private float rotationSpeed = 180f;
    //the rotation axis around the Y axis of the player
    [SerializeField]
    private string yRotationAxis = "Mouse X";
    //the rotation axis around the X axis of the camera.
    [SerializeField]
    private string xRotationAxis = "Mouse Y";

    //the minimum rotation of the camera on X
    [SerializeField]
    private float minRotX = -70;
    //the maximum rotation of the camera on X
    [SerializeField]
    private float maxRotX = 80;

    //the camera that represents the players head.
    [SerializeField]
    private new Transform camera;

    //the current rotation of the camera on the X axis.
    private float currentRotX = 0f;

    //the layermask that we want to use for detection.
    [SerializeField]
    private LayerMask detectionMask;

    //the button we use to interact with objects.
    [SerializeField]
    private string interactButton = "Interact";

    //the button we use to jump!
    [SerializeField]
    private string jumpButton = "Jump";

    //inventory that keeps track of collected items.
    List<Item> inventory;

    //y-axis velocity.
    private float yVelocity;
    //The multiplier for the effectiveness of gravity
    [SerializeField]
    private float gravityMultiplier = 1;
    //the speed at the start of a jump.
    [SerializeField]
    private float jumpSpeed = 3;
    [SerializeField]
    protected float interactTime = 0.7f;
    [SerializeField]
    protected Image progressFiller;

    //the position where we last detected the ground, only used for editor visualization.
    private Vector3 lastGroundHit;

    public bool InvertXRotation { get; set; } = false;

    //initial setup
    private void Start()
    {
        //make sure our inventory is not null!
        inventory = new List<Item>();

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

    ///<summary>
    ///Move the character by the WASD input
    ///</summary>
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
        movement *= speed * Time.deltaTime;

        //better and more reliable ground detection than the character controller's .isGrounded property.
        if(Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit groundHit, 0.6f))
        {
            lastGroundHit = groundHit.point;
            //use our input button to jump!
            if (Input.GetButtonDown(buttonName: jumpButton))
            {
                //set the yVelocity to our jump speed.
                yVelocity = jumpSpeed;
            }
        }
        else
        {
            //accelerate our player on the Y axis as long as he is in the air!
            yVelocity += Physics.gravity.y * Time.deltaTime * gravityMultiplier;
        }
        //add the yVelocity to our movement delta.
        movement.y += yVelocity * Time.deltaTime;

        //Move uses the movement delta we calculated above to move our character in the scene!
        character.Move(movement);
    }

    //Rotate the player along the specified axes of the mouse.
    void Rotation()
    {
        //mouse X movement
        float yRotation = Input.GetAxis(axisName: yRotationAxis);
        yRotation *= rotationSpeed * Time.deltaTime;

        //mouse Y movement
        float xRotation = Input.GetAxis(axisName: xRotationAxis);
        xRotation *= rotationSpeed * Time.deltaTime * (InvertXRotation ? -1f : 1);
        //The ? marks a ternary statement!
        //if the bool is true, it uses the first value (-1), if its false, it uses the second value (1)

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

                //try to find a script we can interact with!
                Tutorial.IInteractable interactable = hit.transform.GetComponent<Tutorial.IInteractable>();

                if (interactable != null) //if the interactable exists
                {
                    StartCoroutine(Interact(interactable, hit.point));
                }
            }
        }
    }

    IEnumerator Interact(IInteractable interactable, Vector3 interactPosition)
    { 
        float startTime = Time.time;
        float progress = 0;

        while(Input.GetButton(buttonName: interactButton))
        {
            float current = Time.time;
            progress = (current - startTime) / interactTime;
            progressFiller.fillAmount = progress;

            if( current - startTime >= interactTime)
            {
                break;
            }
            yield return null;
        }

        if(progress >= 1 && Vector3.Distance(camera.position, interactPosition) <= 2f)
        {
            interactable.Interact(this);
        }
        progressFiller.fillAmount = 0;
    }

    public void AddItemToPlayer(Item item)
    {
        inventory.Add(item);
    }
    public void RemoveItemFromPlayer(Item item)
    {
        inventory.Remove(item);
    }
    public bool DoesPlayerHaveItem(Item item)
    {
        return inventory.Contains(item);
    }

    //Draw a Sphere at the position where we last detected the ground
    //ONLY VISIBLE IN THE SCENE VIEW!
    public void OnDrawGizmos()
    {
        //make the sphere red!
        Gizmos.color = Color.red;
        //Draw the sphere itself. this uses worldspace.
        Gizmos.DrawSphere(lastGroundHit, 0.5f);
    }
}

public class Interactable : MonoBehaviour
{

}