using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    public InputActionReference toggleDoubleRotationAction;

    private bool grabbing = false;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private bool doubleRotation = false;

    private void Start()
    {
        action.action.Enable();
        toggleDoubleRotationAction.action.Enable();

        toggleDoubleRotationAction.action.performed += ToggleDoubleRotation;

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                if (otherHand.grabbing && otherHand.grabbedObject == grabbedObject)
                {
                    Vector3 combinedDeltaPosition = (transform.position - previousPosition + otherHand.transform.position - otherHand.previousPosition) / 2;
                    Quaternion combinedDeltaRotation = Quaternion.Slerp(
                        transform.rotation * Quaternion.Inverse(previousRotation),
                        otherHand.transform.rotation * Quaternion.Inverse(otherHand.previousRotation),
                        0.5f
                    );

                    if (doubleRotation)
                        combinedDeltaRotation = DoubleRotationMagnitude(combinedDeltaRotation);

                    ApplyDeltaTransform(grabbedObject, combinedDeltaPosition, combinedDeltaRotation);
                }
                else
                {
                    Vector3 deltaPosition = transform.position - previousPosition;
                    Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                    if (doubleRotation)
                        deltaRotation = DoubleRotationMagnitude(deltaRotation);

                    ApplyDeltaTransform(grabbedObject, deltaPosition, deltaRotation);
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            grabbedObject = null;
        }
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void ToggleDoubleRotation(InputAction.CallbackContext context)
    {
        doubleRotation = !doubleRotation;
    }

    private Quaternion DoubleRotationMagnitude(Quaternion rotation)
    {
        rotation.ToAngleAxis(out float angle, out Vector3 axis);
        angle *= 2f;
        return Quaternion.AngleAxis(angle, axis);
    }

    private void ApplyDeltaTransform(Transform target, Vector3 deltaPosition, Quaternion deltaRotation)
    {
        target.position += deltaPosition;
        Vector3 offset = target.position - transform.position;
        offset = deltaRotation * offset;
        target.position = transform.position + offset;
        target.rotation = deltaRotation * target.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground
        
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }
}
