using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GUIHelper : MonoBehaviour
{
    [SerializeField]
    private AnimationBlender animationBlender;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animationBlender.ResetAngles();
        }
    }
}
