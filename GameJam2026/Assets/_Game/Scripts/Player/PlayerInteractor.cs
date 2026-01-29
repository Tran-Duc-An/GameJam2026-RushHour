using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    private float m_InteractRange = 1f;
    private LayerMask m_InteractableLayer;

    void Update()
    {
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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