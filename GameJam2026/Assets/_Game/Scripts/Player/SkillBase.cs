using UnityEngine;
using UnityEngine.UI; 

public abstract class SkillBase : MonoBehaviour, ICooldown
{
    [Header("Skill Settings")]
    public string skillName = "New Skill";
    public float cooldownTime = 5f;

    // We track when the cooldown started to calculate the %
    private float cooldownStartTime = -999f; 

    // --- Core Logic ---

    // call this from Update() or Input logic
    public void ActivateSkill()
    {
        // Check if we can use it
        if (!IsReady()) return;

        // 1. Trigger the actual skill (implemented by child scripts)
        OnSkillTriggered();

        // 2. Start the timer
        cooldownStartTime = Time.time;
    }

    // Abstract: The specific skill (like "ThrowPot") must fill this in
    protected abstract void OnSkillTriggered();

    // --- ICooldown Interface Implementation ---
    // This allows the UI script to read the data automatically

    public float GetCooldownFactor()
    {
        // If ready, return 0 (Empty circle)
        if (IsReady()) return 0f;

        // Calculate percentage: (Time Passed / Total Time)
        float timePassed = Time.time - cooldownStartTime;
        float factor = 1f - (timePassed / cooldownTime);
        
        // Clamp it between 0 and 1 just to be safe
        return Mathf.Clamp01(factor);
    }

    public bool IsReady()
    {
        // Check if enough time has passed
        return Time.time >= cooldownStartTime + cooldownTime;
    }

    public string GetAbilityName()
    {
        return skillName;
    }
}