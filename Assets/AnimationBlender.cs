using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBlender : MonoBehaviour
{
    private struct TransformsPare
    {
        public readonly Transform Physical;
        public readonly Transform Animated;

        public TransformsPare(Transform physical, Transform animated)
        {
            Physical = physical;
            Animated = animated;
        }
    }

    [SerializeField] private Transform reference;
    [SerializeField, Range(0, 1)] private float lefpFactor;

    [SerializeField] private List<Transform> ignore;

    private List<TransformsPare> transformsPares;

    void Start()
    {
        transformsPares = new List<TransformsPare>();
        CollectData(transformsPares, transform, reference);
    }

    private static void CollectData(List<TransformsPare> pares, Transform physical, Transform animated)
    {
        for (int i = 0; i < physical.childCount; i++)
        {
            Transform pChild = physical.GetChild(i);
            Transform aChild = animated.GetChild(i);
            Validate(pChild, aChild);
            pares.Add(new TransformsPare(pChild, aChild));
            if (pChild.childCount > 0)
            {
                CollectData(pares, pChild, aChild);
            }
        }
    }

    private static void Validate(Transform transform1, Transform transform2)
    {
        if (transform1.name != transform2.name)
        {
            throw new Exception($"Name of Transforms are not same ({transform1.name} - {transform2.name})");
        }

        if (transform1.childCount != transform2.childCount)
        {
            throw new Exception($"Count of children are not same ({transform1.name})");
        }
    }

    void FixedUpdate()
    {
        foreach (var pare in transformsPares)
        {
            if (!ignore.Contains(pare.Physical))
            {
                pare.Physical.localRotation =
                    Quaternion.Lerp(pare.Physical.localRotation, pare.Animated.localRotation, lefpFactor);
            }
        }
    }
}