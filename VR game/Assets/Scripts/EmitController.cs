using UnityEngine;
using UnityEngine.InputSystem;

public class EmitController : MonoBehaviour
{
    public Hand hand;
    public InputActionReference primaryButtonAction;

    private void Start()
    {
        primaryButtonAction.action.Enable();
        hand.SetEmitWind(0.0f);
    }

    private void Update()
    {
        float primaryButtonValue = primaryButtonAction.action.ReadValue<float>();
        hand.SetEmitWind(primaryButtonValue);
    }
}