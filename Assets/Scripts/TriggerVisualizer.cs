using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisualizer : MonoBehaviour
{
    //ONLY VISIBLE IN THE SCENE VIEW!
    //Draw a Cube that visualizes our Box Trigger.
    private void OnDrawGizmos()
    {
        //save the previous matrix (offset, rotation, scale) of the Gizmos
        Matrix4x4 saveOldMatrix = Gizmos.matrix;
        //make it a 50% transparent green.
        Gizmos.color = new Color(0.4f, 0.9f, 0.4f, 0.5f);
        //Make the Gizmos use the local transformation matrix. (Apply the object's rotation, scale, and position)
        Gizmos.matrix = transform.localToWorldMatrix;
        //Draw the cube at position 0/0/0 (the objects position), with a size of 1/1/1 (a cube)
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        //reset the gizmo matrix.
        Gizmos.matrix = saveOldMatrix;
    }
}
