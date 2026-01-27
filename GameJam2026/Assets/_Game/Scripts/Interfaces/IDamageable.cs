public interface IDamageable 
{
    // Returns true if the damage actually killed the object
    void TakeDamage(int amount);
}