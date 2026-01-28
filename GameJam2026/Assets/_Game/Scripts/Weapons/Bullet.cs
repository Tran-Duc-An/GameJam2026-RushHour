using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto destroy after 2 sec
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Use the Interface!
        IDamageable enemy = hitInfo.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        
        Destroy(gameObject); // Destroy bullet on impact
    }
}