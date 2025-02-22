using UnityEngine;

public class Hole : MonoBehaviour
{
    public ParticleSystem particleEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        particleEffect.Play();
    }
}