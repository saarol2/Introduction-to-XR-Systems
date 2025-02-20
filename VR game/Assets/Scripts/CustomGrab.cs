using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Use the GrabPoint if it exists
                Transform grabPoint = grabbedObject.Find("GrabPoint");
                if (grabPoint)
                {
                    grabbedObject.position = transform.position - (grabPoint.position - grabbedObject.position);
                    grabbedObject.rotation = transform.rotation * Quaternion.Inverse(grabPoint.rotation) * grabbedObject.rotation;
                }
                else
                {
                    grabbedObject.position = transform.position;
                    grabbedObject.rotation = transform.rotation;
                }

                // Disable Rigidbody while grabbing
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            // Enable Rigidbody when releasing
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }
}