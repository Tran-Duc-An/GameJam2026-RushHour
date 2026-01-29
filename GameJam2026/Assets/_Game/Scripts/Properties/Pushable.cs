using UnityEngine;

public class Pushable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float m_PushForce = 5f;
    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public bool CanInteract(PlayerInteractor player)
    {
        return true;
    }

    public void Interact(PlayerInteractor player)
    {
        Debug.Log("Pushing object");

        Vector2 pushDirection = (transform.position - player.transform.position).normalized;
        m_Rigidbody.AddForce(pushDirection * m_PushForce, ForceMode2D.Impulse);
    }
}