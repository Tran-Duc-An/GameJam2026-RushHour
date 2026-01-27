# 🎮 Game Jam 2026 - Developer Guide

Welcome to the repo! This document outlines where files go and how we can work together efficiently without breaking the project.

## 📂 Folder Structure
**Everything we make goes inside `Assets/_Game/`.** Do not modify `Plugins` or `Packages` unless we are adding a new tool.

```text
Assets/
├── _Game/                  <-- 🟢 ALL OUR CUSTOM WORK
│   ├── Animations/         <-- Controllers & Clips
│   ├── Art/                <-- Sprites, Textures, Materials, Models
│   ├── Audio/              <-- SFX and Music
│   ├── Prefabs/            <-- ⭐️ EDIT THESE, NOT SCENES!
│   │   ├── Characters/     (Player, Enemies)
│   │   ├── Environment/    (Platforms, Props)
│   │   └── UI/
│   └── Scripts/            <-- C# Code
│       ├── Player/
│       ├── Enemy/
│       └── Interfaces/       (GameManager, Audio, UI)
│
├── Plugins/                <-- 🔴 3rd Party Assets (Do not edit scripts here)
├── Scenes/                 <-- Game Levels
│   ├── _Sandbox/           <-- Create your own test scene here (e.g., "Dev_Test")
│   └── Levels/             <-- Final Game Scenes
└── Settings/               <-- Unity Project Settings (Input, URP, etc.)
```

# Tips and tricks

## 1. The "Prefab" Rule
* Never build the player or enemy logic directly in the Scene.
* Make them Prefabs and store them in _Game/Prefabs/.
* If you need to change the Player's speed or sprite, open the Prefab, edit it, and save. This updates it everywhere automatically.


## 2. The "Sandbox" Rule (Avoid Merge Conflicts)

* Avoid working in the Main Scene simultaneously. Unity cannot merge scene files well.
* Create a generic scene for yourself in Assets/Scenes/_Sandbox/ (e.g., Test_Movement.unity).
* Test your mechanics there. When it works, turn it into a Prefab and drop it into the Main Scene.
# Example: 
* Coder 1 works in TestScene_Player.
* Coder 2 works in TestScene_Guns.
* Coder 3 works in TestScene_AI.


## 3. The Interface Rule (The "Universal Adapter")
To prevent our code from becoming a tangled mess ("Spaghetti Code"), we use Interfaces. This allows different systems (like Weapons and Enemies) to talk to each other without knowing exactly what the other is.

# The Golden Rule:

* Bullets do NOT know about "Enemies".
* Bullets do NOT know about "Crates".
* Bullets ONLY know about "Things that can take damage".

# Example
* Create a public interface
```text
// This is not a class, it's a contract.
// Anything that uses this MUST have a TakeDamage function.
public interface IDamageable 
{
    void TakeDamage(int amount);
}
```
* Use it in Enemy
```text
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int currentHealth = 100;

    // The interface forces us to write this function
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Ouch! I took " + amount + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
```
* Use it in Player

```text
using UnityEngine;
using UnityEngine.Events; // Needed for UnityEvents
using System.Collections;

public class Health : MonoBehaviour, IDamageable
{

    // This is the implementation of your Interface!
    public void TakeDamage(int damageAmount)
    {
        // 1. If we are currently invincible, ignore damage
        if (isInvincible) return;

        // 2. Reduce Health
        currentHealth -= damageAmount;
        
        // 3. Trigger events (Play sound, shake camera, update UI)
        OnTakeDamage.Invoke();
        Debug.Log($"{gameObject.name} took {damageAmount} damage. HP: {currentHealth}");

        // 4. Check for Death
        if (currentHealth <= 0)
        {
            Die();
        }
        else if (useIFrames)
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }
}
```
