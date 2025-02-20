using UnityEngine;
using UnityEngine.InputSystem;

public class EmitController : MonoBehaviour
{
    public Hand hand;
    public InputActionReference primaryButtonAction;

    private void Start()
    {
        primaryButtonAction.action.Enable();
        hand.SetEmitWind(0.0f); // Aseta EmitWind-parametri alussa nollaksi
    }

    private void Update()
    {
        float primaryButtonValue = primaryButtonAction.action.ReadValue<float>(); // Tarkista primary buttonin tila
        hand.SetEmitWind(primaryButtonValue); // Aseta EmitWind tila

        // Debug-tulostus
        Debug.Log("Primary Button Value: " + primaryButtonValue);
    }
}