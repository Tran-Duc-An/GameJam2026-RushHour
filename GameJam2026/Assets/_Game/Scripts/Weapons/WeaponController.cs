using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Assign the pivot object (the child object that holds the gun)")]
    public Transform weaponPivot;

    // This holds the Interface, so we don't care if it's a Pistol or a Rocket Launcher
    private IWeapon currentWeapon;

    void Start()
    {
        // Find the gun attached to the pivot at start
        // In a real game, you would call this when picking up a new gun
        currentWeapon = weaponPivot.GetComponentInChildren<IWeapon>();
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        // 1. Get Mouse Position in World Space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // 2. Calculate Direction (Mouse - Pivot)
        Vector2 direction = (mousePos - weaponPivot.position).normalized;

        // 3. Calculate Angle (Atan2 returns radians, convert to degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply Rotation
        weaponPivot.rotation = Quaternion.Euler(0, 0, angle);

        // 5. FLIPPING (Crucial for 2D)
        // If we are looking left (angle > 90 or < -90), flip the gun upside down
        // so the sprite looks correct.
        if (angle > 90 || angle < -90)
        {
            weaponPivot.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponPivot.localScale = new Vector3(1, 1, 1);
        }
    }

    void HandleShooting()
    {
        // If we don't have a gun, do nothing
        if (currentWeapon == null) return;

        // "0" is Left Click
        if (Input.GetMouseButton(0))
        {
            currentWeapon.Attack();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            currentWeapon.StopAttack();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }
    }

    // Helper function for when your teammate makes the "Pickup" system
    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        // Destroy old gun
        foreach(Transform child in weaponPivot) Destroy(child.gameObject);

        // Spawn new gun
        GameObject gun = Instantiate(newWeaponPrefab, weaponPivot);
        currentWeapon = gun.GetComponent<IWeapon>();
    }
}