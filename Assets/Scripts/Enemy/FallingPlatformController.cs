using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float fallSpeed = 5f;
    private float disableHeight = -15f;
    private bool shouldFall = false;
    private Rigidbody rb;
    private AudioSource fallAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.isKinematic = true;// set kinematic for floor don`t falling

        fallAudioSource = GetComponent<AudioSource>();
        
        if (fallAudioSource == null)
        {
            Debug.LogError("AudioSource for falling sound is not attached to the game object.");
        }
    }

    void Update()
    {
        if (shouldFall)
        {
            rb.isKinematic = false; // set kinematic for fall
            rb.velocity = new Vector3(0, -fallSpeed, 0);
        }

        if (transform.position.y < disableHeight)
        {
            DisablePlatform();
        }
    }

    public void TriggerFall()
    {
        Invoke("StartFalling", fallDelay); // Start falling after a delay
    }

    private void StartFalling()
    {
        shouldFall = true;// Set flag to start falling
        if (fallAudioSource != null && fallAudioSource.gameObject.activeInHierarchy)
        {
            fallAudioSource.Play();
            Debug.Log("Falling sound played.");
        }
    }

    private void DisablePlatform()
    {
        gameObject.SetActive(false);
        Debug.Log("Platform disabled.");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TriggerFall();
            Debug.Log("Platform triggered to fall.");
        }
    }
}
