using UnityEngine;
using System.Collections;

public class TriggerPlate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlatformController platformController;
    [SerializeField] private float delayBeforeRaise = 1f;
    [SerializeField] private float delayBeforeLower = 1f;
    [SerializeField] private AudioSource audioSource;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(RaisePlatformWithDelay());
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(LowerPlatformWithDelay());
        }
    }

    IEnumerator RaisePlatformWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeRaise);
        platformController.RaisePlatform();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Platform raised and sound played.");
        }
    }

    IEnumerator LowerPlatformWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLower);
        platformController.LowerPlatform();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("Platform lowered and sound played.");
        }
    }
}
