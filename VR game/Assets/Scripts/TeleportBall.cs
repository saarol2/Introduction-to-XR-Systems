using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPlayerToBall : MonoBehaviour
{
    public InputActionReference teleportAction;
    public Transform player;
    public Transform ball;

    void OnEnable()
    {
        teleportAction.action.Enable();
        teleportAction.action.performed += OnTeleportActionPerformed;
    }

    void OnDisable()
    {
        teleportAction.action.performed -= OnTeleportActionPerformed;
        teleportAction.action.Disable();
    }

    void OnTeleportActionPerformed(InputAction.CallbackContext context)
    {
        TeleportPlayerToBallPosition();
    }

    void TeleportPlayerToBallPosition()
    {
        player.position = ball.position + Vector3.back * 0.5f;
    }
}