using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int m_Health = 1;

    public void TakeDamage(int amount)
    {
        m_Health -= amount;
        if (m_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}