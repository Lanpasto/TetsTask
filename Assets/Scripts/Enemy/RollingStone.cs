using UnityEngine;

public class RollingStone : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource fallingSound;
    private bool hasPlayedSound = false;
    [SerializeField] private LayerMask groundLayer;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object's layer is part of the groundLayer mask 
// and ensure the falling sound has not already been played.
        if (groundLayer == (groundLayer | (1 << collision.gameObject.layer)) && !hasPlayedSound)
        {
            fallingSound.Play();
            hasPlayedSound = true;
            Debug.Log("Falling sound played.");
        }
    }
}

