using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference action;
    public Transform externalViewPoint;
    private Vector3 initialPosition;
    private bool switched = false;

    void Start()
    {
        initialPosition = transform.position;
        action.action.Enable();
        action.action.performed += SwitchPosition;
    }

    private void SwitchPosition(InputAction.CallbackContext ctx)
    {
        if (!switched)
        {
            transform.position = externalViewPoint.position;
        }
        else
        {
            transform.position = initialPosition;
        }
        switched = !switched;
    }
}