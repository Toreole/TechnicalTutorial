using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TransformDemo : MonoBehaviour
    {
        [SerializeField]
        Transform parent;

        [SerializeField]
        Vector3 point;
        [SerializeField]
        Vector3 direction; //local space.

        [SerializeField]
        Vector3 forwardDirection, upDirection;
        [SerializeField]
        Transform plane;

        [SerializeField]
        Rigidbody body;

        public Vector3 Forward 
        {
            get => forwardDirection;
            set => forwardDirection = value;
        }
        public Vector3 Up 
        {
            get => upDirection;
            set => upDirection = value;
        }

        private void Start() 
        {
            this.Hello();
            Debug.Log("hello", this);

        }

        private int x, y;

        void Stuff(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Update is called once per frame
        void Update()
        {
            //Vector3 position = transform.TransformPoint(point);
            //Debug.DrawLine(transform.position, position, Color.red);

            //Vector3 dir = transform.TransformDirection(direction);
            //Debug.DrawRay(transform.position, dir);

            //Vector3 dir2 = transform.TransformVector(direction);
            //Debug.DrawRay(transform.position, dir2, Color.blue);
            Vector3 forwardNormal = forwardDirection.normalized, upNormal =  upDirection.normalized;

            transform.rotation = Quaternion.LookRotation(forwardNormal, upNormal);
            plane.up = transform.right;

            Debug.DrawRay(transform.position, forwardNormal, Color.blue);
            Debug.DrawRay(transform.position, Vector3.Cross(forwardNormal, upNormal).normalized, Color.red);
            Debug.DrawRay(transform.position, upNormal, Color.green);
        }

        void ChildTest()
        {
            //for(int i = 0; i < parent.childCount; i++)
            //    Debug.Log(parent.GetChild(i).name);

            foreach(Transform t in parent)
            {
                Debug.Log(t.name);
            }

            for(int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}