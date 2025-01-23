using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDecal : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public SprayPlace sprayPlaceScript; // Reference to the SprayPlace script
    public string firstAnimationName; // Name of the first animation in the sequence
    public string lastAnimationName; // Name of the last animation in the sequence

    private bool hasLooped;

    void Update()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the animation has reached the end and is the last animation
            if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName(lastAnimationName) && !hasLooped)
            {
                animator.Play(firstAnimationName, 0, 0f); // Restart from the first animation
                hasLooped = true;

                // Call the function to remove all decals
                if (sprayPlaceScript != null)
                {
                    sprayPlaceScript.RemoveAllDecals();
                }
            }
            else if (!stateInfo.IsName(lastAnimationName))
            {
                hasLooped = false; // Reset loop condition
            }
        }
    }
}
