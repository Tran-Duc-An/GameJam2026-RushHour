using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 5f;
    private Rigidbody2D m_Rigidbody;
    private Vector2 m_MoveInput;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        m_Rigidbody.linearVelocity = m_MoveInput * m_MoveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_MoveInput = context.ReadValue<Vector2>();
    }
}
