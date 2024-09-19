using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetPosition;
    [Header("Settings")]
    [SerializeField] private float speed;
    private Vector3 initialPosition;
    private bool shouldRise = false;
    private bool shouldLower = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // If the platform should rise
        if (shouldRise)
        {
             // Move the platform towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            // Check if the platform has reached the target position
            if (transform.position == targetPosition.position)
            {
                shouldRise = false;
                Debug.Log("Platform reached target position.");
            }
        }
        else if (shouldLower)
        {   
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            if (transform.position == initialPosition)
            {
                shouldLower = false;
                Debug.Log("Platform returned to initial position.");
            }
        }
    }

    public void RaisePlatform()
    {
       shouldRise = true; // Set the flag to rise
        shouldLower = false; // Ensure it doesn't lower at the same time
        Debug.Log("Platform is rising.");
    }

    public void LowerPlatform()
    {
        shouldLower = true;
        shouldRise = false;
        Debug.Log("Platform is lowering.");
    }
}
