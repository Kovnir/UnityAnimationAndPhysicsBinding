using System;
using System.Collections.Generic;
using UnityEngine;

//github: https://github.com/Kovnir/UnityAnimationAndPhysicsBinding
public class AnimationBlender : MonoBehaviour
{
    private struct TransformsPare
    {
        public readonly Transform Physical;
        public readonly Transform Animated;
        public Quaternion LastRotation;

        public TransformsPare(Transform physical, Transform animated)
        {
            Physical = physical;
            Animated = animated;
            LastRotation = animated.localRotation;
        }
    }

    [Tooltip("Object for taking animation")]
    [SerializeField] private Transform reference;
    [Tooltip("Should we fail if bones name are not equal")]
    [SerializeField] private bool checkBonesNames = true;
    [Tooltip("How much animation updates should be applied")]
    [SerializeField, Range(0, 1)] private float animationFactor;
    [Tooltip("How much physic body should try to return to animation angles (bigger values - closer to animation, but bigger jitter with physic)")]
    [SerializeField, Range(0, 1)] private float resolveFactor;

    [Tooltip("A list of Transforms to ignore (animation will not be applied)")]
    [SerializeField] private List<Transform> ignore;

    private List<TransformsPare> transformsPares;

    void Start()
    {
        transformsPares = new List<TransformsPare>();
        CollectData(transformsPares, transform, reference, checkBonesNames);
    }

    private static void CollectData(List<TransformsPare> pares, Transform physical, Transform animated, bool checkBonesNames)
    {
        ValidateChildCount(physical, animated);
        for (int i = 0; i < physical.childCount; i++)
        {
            Transform pChild = physical.GetChild(i);
            Transform aChild = animated.GetChild(i);
            if (checkBonesNames)
            {
                ValidateName(pChild, aChild);
            }

            pares.Add(new TransformsPare(pChild, aChild));
            if (pChild.childCount > 0)
            {
                CollectData(pares, pChild, aChild, checkBonesNames);
            }
        }
    }

    private static void ValidateName(Transform transform1, Transform transform2)
    {
        if (transform1.name != transform2.name)
        {
            throw new Exception($"Name of Transforms are not same ({transform1.name} - {transform2.name})");
        }
    }
    private static void ValidateChildCount(Transform transform1, Transform transform2)
    {
        if (transform1.childCount != transform2.childCount)
        {
            throw new Exception($"Count of children are not same ({transform1.name})");
        }
    }

    public void ResetAngles()
    {
        for (int index = 0; index < transformsPares.Count; index++)
        {
            var pare = transformsPares[index];
            pare.Physical.localRotation = pare.Animated.localRotation;
        }
    }

    void FixedUpdate()
    {
        for (int index = 0; index < transformsPares.Count; index++)
        {
            var pare = transformsPares[index];
            if (!ignore.Contains(pare.Physical))
            {
                //difference between last angles and new angles
                var dif = Quaternion.Inverse(pare.LastRotation) * (pare.Animated.localRotation);
                //make difference smaller and apply
                pare.Physical.localRotation *= Quaternion.Slerp(Quaternion.identity, dif, animationFactor);
                //lerp with reference value
                pare.Physical.localRotation = Quaternion.Slerp(pare.Physical.localRotation, pare.Animated.localRotation, resolveFactor);
                
                //save rotations values
                pare.LastRotation = pare.Animated.localRotation;
                transformsPares[index] = pare;
                
                // var dif = Quaternion.Inverse(pare.Physical.localRotation) * pare.Animated.localRotation;
                // pare.Physical.localRotation *= Quaternion.Slerp(Quaternion.identity, dif, lerpFactor);

                // pare.Physical.localRotation =
                //     Quaternion.Lerp(pare.Physical.localRotation, pare.Animated.localRotation, lefpFactor);
            }
        }
    }
}