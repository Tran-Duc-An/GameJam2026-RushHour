using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    private float m_InteractRange = 5f;
    private LayerMask m_InteractableLayer;

    void Start()
    {
        m_InteractableLayer = LayerMask.GetMask("Default");
    }

    void Update()
    {
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action triggered");

        if (context.started)
        {
            Debug.Log("Attempting to interact with nearby objects");

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_InteractRange, m_InteractableLayer);
            foreach (var hit in hits)
            {
                IInteractable interactable = hit.GetComponent<IInteractable>();
                if (interactable != null && interactable.CanInteract(this))
                {
                    interactable.Interact(this);
                    break;
                }
            }
        }
    }
}