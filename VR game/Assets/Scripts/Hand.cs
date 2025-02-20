using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private float emitWindTarget; // Uusi muuttuja
    private float emitWindCurrent; // Uusi muuttuja
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string animatorEmitWindParam = "EmitWind"; // Uusi parametri

    void Start()
    {
        animator = GetComponent<Animator>();
        // Aseta EmitWind-parametri alussa nollaksi
        animator.SetFloat(animatorEmitWindParam, 0.0f);
    }

    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    internal void SetEmitWind(float v) // Uusi metodi
    {
        emitWindTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (emitWindCurrent != emitWindTarget) // Uusi logiikka
        {
            emitWindCurrent = Mathf.MoveTowards(emitWindCurrent, emitWindTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorEmitWindParam, emitWindCurrent);
        }
    }
}