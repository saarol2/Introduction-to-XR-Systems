using UnityEngine;
using UnityEngine.InputSystem;

public class WindHand : MonoBehaviour
{
    public float windForce = 5f;
    public Transform windDirection;
    public InputActionReference blowAction;
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public ParticleSystem windParticles;
    public float windSpeedMultiplier = 1.0f;

    private bool isBlowing = false;

    private void Start()
    {
        blowAction.action.Enable();
        gripAction.action.Enable();
        triggerAction.action.Enable();
        windParticles.Stop();
    }

    private void Update()
    {
        bool isGripping = gripAction.action.ReadValue<float>() > 0;
        bool isTriggering = triggerAction.action.ReadValue<float>() > 0;
        
        if (isGripping || isTriggering)
        {
            isBlowing = false;
            windParticles.Stop();
            return;
        }
        isBlowing = blowAction.action.ReadValue<float>() > 0.5f;

        if (isBlowing && !windParticles.isPlaying)
        {
            windParticles.Play();
        }
        else if (!isBlowing && windParticles.isPlaying)
        {
            windParticles.Stop();
        }
        UpdateParticleProperties();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isBlowing && other.CompareTag("Ball"))
        {
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                Vector3 forceDirection = -windDirection.right;
                ballRb.AddForce(forceDirection * windForce, ForceMode.Impulse);
            }
        }
    }

    private void UpdateParticleProperties()
    {
        var main = windParticles.main;
        main.startSize = new ParticleSystem.MinMaxCurve(0.05f, 0.25f);
        var emission = windParticles.emission;
        emission.rateOverTime = 40;
        windParticles.playbackSpeed = windSpeedMultiplier;
    }
}