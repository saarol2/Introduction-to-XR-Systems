using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public Hand hand;
    private CustomGrab otherHand;

    public InputActionReference gripAction;
    public InputActionReference triggerAction;

    private void Start()
    {
        gripAction.action.Enable();
        triggerAction.action.Enable();

        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    private void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        float triggerValue = triggerAction.action.ReadValue<float>();

        hand.SetGrip(gripValue);
        hand.SetTrigger(triggerValue);
    }
}