using System;
using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    private Rigidbody cahsedRigidbody;
    private int collisionsCount;
    private void Awake()
    {
        cahsedRigidbody = GetComponent<Rigidbody>();
        cahsedRigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<RigidbodyController>() == null)
        {
            collisionsCount++;
            cahsedRigidbody.isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<RigidbodyController>() == null)
        {
            collisionsCount--;
            if (collisionsCount == 0)
            {
                cahsedRigidbody.isKinematic = false;
            }
        }
    }
}