using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CollectionTestScript : MonoBehaviour
{
    [SerializeField] // a simple array that has 5 "slots" by default.
    GameObject[] gameObjectArray = new GameObject[5];

    [SerializeField] //a simple list of GameObjects. Lists are nicer than arrays for a bunch of use cases.
    List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField] //prefabs should be kept seperately from runtime created objects to avoid confusion and unwanted changes.
    List<GameObject> prefabs;

    private void Start()
    { 
        //loop over all the slots in the array.
        foreach(GameObject obj in gameObjectArray)
        {
            //! Array entries can be null (if they are reference types)
            //we dont want to use null objects.
            if (obj == null)
                break;

            obj.transform.Translate(new Vector3(10, 1, 0));
        }
        //foreach is smart with lists, as it only uses the existing objects, if there are none, it wont even try to call the Translate method.
        foreach(GameObject obj in gameObjects)
        {
            obj.transform.Translate(Vector3.up);
        }
    }
}
