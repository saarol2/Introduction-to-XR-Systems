using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public InputActionReference restartAction;

    void OnEnable()
    {
        restartAction.action.Enable();
        restartAction.action.performed += OnRestartActionPerformed;
    }

    void OnDisable()
    {
        restartAction.action.performed -= OnRestartActionPerformed;
        restartAction.action.Disable();
    }

    void OnRestartActionPerformed(InputAction.CallbackContext context)
    {
        Restart();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}