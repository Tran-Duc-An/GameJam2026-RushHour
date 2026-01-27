using UnityEngine;

public interface IKnockbackable
{
    // Push the object away from the source of damage
    void ApplyKnockback(Vector2 direction, float force);
}