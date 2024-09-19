using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [Header("Enemy Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectionRadius;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is not attached to the enemy.");
        }
    }

    void Update()
    {// Check if the player is within detection radius
        if (Vector3.Distance(player.position, transform.position) <= detectionRadius)
        {
            MoveTowardsPlayer();// Move enemy towards the player
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;// Calculate direction to player
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);// Set enemy velocity
        Debug.Log("Enemy moving towards player.");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
