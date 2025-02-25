using UnityEngine;

public class Hole : MonoBehaviour
{
    public ParticleSystem particleEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            EndGame(other.gameObject);
        }
    }

    void EndGame(GameObject ball)
    {
        particleEffect.Play();
        Destroy(ball);
    }
}