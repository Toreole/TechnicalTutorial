using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Tutorial 
{
    public class PostProcessChange : MonoBehaviour
    {
        [SerializeField]
        PostProcessProfile profile;

        private void Start()
        {
            AmbientOcclusion ambientOcclusion;
            if(profile.TryGetSettings<AmbientOcclusion>(out ambientOcclusion))
            {
                ambientOcclusion.active = true;
            }
        }
    }
}