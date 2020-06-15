using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class AnimationTrigger : MonoBehaviour
    {
        [SerializeField]
        protected bool onlyPlayOnce = true;

        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected string toggleName;

        private int toggleID;

        private void Awake()
        {
            toggleID = Animator.StringToHash(toggleName);
        }

        private void OnTriggerEnter(Collider other)
        {
            animator.SetTrigger(toggleID);
            if (onlyPlayOnce)
                Destroy(this);
        }
    }
}