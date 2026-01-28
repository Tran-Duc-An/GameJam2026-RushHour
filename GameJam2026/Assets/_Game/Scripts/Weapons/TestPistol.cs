using UnityEngine;

public class TestPistol : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Where the bullet comes out
    public float fireRate = 0.5f;

    private float nextFireTime = 0;

    public void Attack()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Debug.Log("Bang!");
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void StopAttack() { } // Pistols don't care about release
    public void Reload() { Debug.Log("Reloading..."); }
}