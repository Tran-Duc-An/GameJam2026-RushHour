using UnityEngine;

public class Grabbable : MonoBehaviour, IInteractable
{
    private bool m_IsHeld = false;
    private GameObject m_OriginalParent;

    public bool CanInteract(PlayerInteractor player)
    {
        return !m_IsHeld || m_OriginalParent == player.gameObject;
    }

    public void Interact(PlayerInteractor player)
    {
        if (!m_IsHeld)
        {
            m_OriginalParent = transform.parent.gameObject;
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero;
            m_IsHeld = true;
        }
        else
        {
            transform.SetParent(m_OriginalParent.transform);
            m_IsHeld = false;
        }
    }
}