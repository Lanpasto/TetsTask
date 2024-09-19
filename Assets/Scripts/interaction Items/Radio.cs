using System.Collections;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;
    private int interactionCount = 0;
    private bool isLocked = false;
    private bool isPlaying = false;
    private AudioClip gachiClip;
    private AudioClip codeClip;
    private AudioClip numberClip;
    private bool canInteract = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gachiClip = Resources.Load<AudioClip>("gachi");
        codeClip = Resources.Load<AudioClip>("code");
        numberClip = Resources.Load<AudioClip>("number");
    }

    public void Interact()
{
    if (!canInteract)// If interaction is not allowed, exit
        return;

    if (isLocked)// Check if the radio is locked
    {
        if (isPlaying)// If audio is playing, off radio
            StopPlayback();

        return; // Exit if locked
    }

    Debug.Log($"Interaction count: {interactionCount}, Locked: {isLocked}");

    interactionCount++;// Increment E push
    canInteract = false;// Disable further interaction until reset
        
    // Determine which audio clip to play based on interaction count
    switch (interactionCount)
    {
        //don`t use this
        case >= 10:
            PlayClip(gachiClip);
            isLocked = true;// Lock further interactions
          
            break;
            
        case >= 4:
       
            PlayClip(codeClip);
            
            break;
        default:
      
            PlayClip(numberClip);
           
            break;
    }

    StartCoroutine(ResetInteraction());
}

private IEnumerator ResetInteraction()// Coroutine to reset interaction after a delay
{
    yield return new WaitForSeconds(1f); 
    canInteract = true;
}

    private void PlayClip(AudioClip clip) // Method to play audio clip
    {
        isPlaying = false; // Reset isPlaying flag
        if (!isPlaying) // If not already playing
        {
            audioSource.clip = clip;
            audioSource.Play();
            isPlaying = true;
            Debug.Log($"Playing clip: {clip.name}");
        }
    }

    private void StopPlayback()// Method to stop audio playback
    {
         audioSource.Stop(); // Stop the audio source
        isPlaying = false; // Reset isPlaying flag
        Debug.Log("Playback stopped.");
    }
}