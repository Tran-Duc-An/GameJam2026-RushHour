using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    private bool m_IsHeld = false;
    private Transform m_OriginalParent;
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool CanInteract(PlayerInteractor player)
    {
        // return !m_IsHeld || m_OriginalParent == player.transform;
        return true;
    }

    public void Interact(PlayerInteractor player)
    {
        if (!m_IsHeld)
        {
            m_OriginalParent = transform.parent;
            m_Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            transform.position = player.GrabPoint.position;
            transform.SetParent(player.GrabPoint);
            m_IsHeld = true;
        }
        else
        {
            transform.SetParent(m_OriginalParent);
            m_Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            m_IsHeld = false;
        }
    }
}