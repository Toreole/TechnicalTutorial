using UnityEngine;

namespace Tutorial
{
    public class AnimationInteract : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected float interactionCooldown;
        [SerializeField]
        protected string toggleName;

        private int toggleID;
        private float lastInteraction;

        private void Awake()
        {
            toggleID = Animator.StringToHash(toggleName);
        }

        public void Interact(PlayerController player)
        {
            if(Time.time - lastInteraction > interactionCooldown)
            {
                lastInteraction = Time.time;
                animator.SetTrigger(toggleID);
            }
        }
    }
}