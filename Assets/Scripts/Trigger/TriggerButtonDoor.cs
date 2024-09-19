using UnityEngine;

public class TriggerButtonDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource doorAudioSource;
    private string openParameterName = "isOpen";
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //start animation one time
            doorAnimator.SetBool(openParameterName, true);
            if (doorAudioSource != null && doorAudioSource.gameObject.activeInHierarchy)
            {//play sound
                doorAudioSource.Play();
                Debug.Log("Door opened and sound played.");
            }
            else
            {
                Debug.LogWarning("AudioSource is not assigned or not active.");
            }
        }
    }
}
