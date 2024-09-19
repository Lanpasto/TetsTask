using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class PlayerInteractor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform InteractorSource;
    PlayerInput playerInput;
    private float InteractRange = 2f;
    InputAction interaction;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interaction = playerInput?.actions.FindAction("Interaction");
        if (interaction == null)
        Debug.LogError("Interaction action not found in PlayerInput.");
    }

    void Update()
    {
        if (interaction != null && interaction.WasPressedThisFrame())// check if interaction was pressed in this frame
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);// create a ray from the interactor source
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) // cast ray to check for hits in the range
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))// check hit object is interactable
                {
                    interactObj.Interact();
                }
            }
        }
    }
}