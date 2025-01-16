using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    public InputActionReference action;
    private bool isRed = false;
    void Start()
    {
        light = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += ToggleLightColor;
    }

    private void ToggleLightColor(InputAction.CallbackContext ctx)
    {
        if (isRed)
        {
            light.color = Color.white;
        }
        else
        {
            light.color = Color.red;
        }
        
        isRed = !isRed;
    }
}