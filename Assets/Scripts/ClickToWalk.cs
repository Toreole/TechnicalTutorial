using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tutorial
{
    public class ClickToWalk : MonoBehaviour
    {
        [SerializeField]
        protected NavMeshAgent player;
        [SerializeField]
        protected new Camera camera;
        [SerializeField]
        protected LayerMask groundMask;
        [SerializeField]
        protected NavMeshSurface navMesh;

        private void Start()
        {
            navMesh.BuildNavMesh();
        }

        private void Update() 
        {
            if(Input.GetMouseButtonDown(1))
            {
                Vector3 inputPosition = Input.mousePosition;

                Ray ray = camera.ScreenPointToRay(inputPosition);

                if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
                {
                    player.SetDestination(hit.point);
                }
            }
        }
    }
}