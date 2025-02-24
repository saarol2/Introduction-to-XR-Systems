using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public InputActionReference selectAction;
    public InputActionReference activateAction;

    public Hand hand;

    private void OnEnable()
    {
        selectAction.action.Enable();
        activateAction.action.Enable();
    }

    private void OnDisable()
    {
        selectAction.action.Disable();
        activateAction.action.Disable();
    }

    void Update()
    {
        hand.SetGrip(selectAction.action.ReadValue<float>());
        hand.SetTrigger(activateAction.action.ReadValue<float>());
    }
}