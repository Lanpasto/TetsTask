using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    PlayerInput playerInput;
    Rigidbody rb;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement Settings")]
    [SerializeField] private int speed;
    [SerializeField] private float sensitivity;
    private float verticalLookRotation = 0f;

    [Header("Input Actions")]
    InputAction moveAction;
    InputAction lookAction;
    InputAction runAction;
    private int runSpeed = 10;
    private int stepSpeed = 5;

    private bool isRunning = false;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        lookAction = playerInput.actions.FindAction("Look");
        runAction = playerInput.actions.FindAction("Run");
    }

    void Update()
    {
        LookPlayer();
        HandleRunning();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        if (direction.magnitude > 0 && !isMoving)
        {
            isMoving = true;
            Debug.Log("Start Moving");
            AudioManager.Instance.PlaySFX("step", true);
        }
        else if (direction.magnitude == 0 && isMoving)
        {
            isMoving = false;
            Debug.Log("Stop Moving");
            AudioManager.Instance.StopSFX();
        }
        // Move player using Rigidbody based on input and camera orientation
        Vector3 moveDirection = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        moveDirection.y = 0;
        rb.velocity = moveDirection.normalized * speed + new Vector3(0, rb.velocity.y, 0);
    }

    void HandleRunning()
    {
        if (runAction.IsPressed() && isMoving && !isRunning) // if pressed E and moving and wasn't run before
        {
            isRunning = true;
            Debug.Log("Player start run");
            AudioManager.Instance.PlaySFX("run", true);
            speed = runSpeed;
        }
        else if ((!runAction.IsPressed() || !isMoving) && isRunning)
        {
            isRunning = false;
            Debug.Log("Player stop run");
            AudioManager.Instance.StopSFX();
            speed = stepSpeed;
        }
    }
    //Defeat after collision with layer Enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.instance.GameOver();
        }
    }
    // Handles player and camera rotation based on mouse movement
    void LookPlayer()
    {
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up, mouseDelta.x * sensitivity * Time.deltaTime);
        verticalLookRotation -= mouseDelta.y * sensitivity * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }
}
