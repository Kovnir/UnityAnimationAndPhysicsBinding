using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GUIHelper : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AnimationBlender animationBlender;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
//            animationBlender.ResetAngles();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("Next");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animationBlender.ResetAngles();
        }
    }
}
