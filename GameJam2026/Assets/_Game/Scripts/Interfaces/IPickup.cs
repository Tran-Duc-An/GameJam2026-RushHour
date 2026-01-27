using UnityEngine;

public interface IPickup
{
    // Called when the player's collider touches the item
    void OnPickup(GameObject player);
}