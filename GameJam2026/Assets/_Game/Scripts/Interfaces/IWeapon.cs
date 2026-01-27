using UnityEngine;

public interface IWeapon
{
    // Called when the player holds down the fire button
    void Attack();

    // Called when the player releases the fire button
    void StopAttack();

    // Called when the player tries to reload
    void Reload();
}