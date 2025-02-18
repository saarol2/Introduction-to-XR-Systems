using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    // Määritellään toisen käden viittaus
    public Hand hand;
    private CustomGrab otherHand;

    // Input Action References
    public InputActionReference gripAction;
    public InputActionReference triggerAction;

    private void Start()
    {
        // Aktivoi input-toiminnot
        gripAction.action.Enable();
        triggerAction.action.Enable();

        // Etsitään toinen käsi (CustomGrab)
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    private void Update()
    {
        // Haetaan gripin ja triggerin arvot syötteistä
        float gripValue = gripAction.action.ReadValue<float>();
        float triggerValue = triggerAction.action.ReadValue<float>();

        // Päivitetään käden animaatioarvot
        hand.SetGrip(gripValue);
        hand.SetTrigger(triggerValue);

        // Voit laajentaa logiikkaa tarvittaessa, esim. kaappaamisen tarkistaminen
        // voidaan liittää tähän muihin käsin tapahtuvin toiminnoin.
    }
}
