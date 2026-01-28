public interface ICooldown 
{
    // Returns a value between 0 (Ready) and 1 (Full Cooldown)
    // Used for Image.fillAmount
    float GetCooldownFactor();
    
    // Returns true if the skill/dash is ready to use
    bool IsReady();
    
    // Optional: Returns the unique ID/Name if you need to find specific icons
    string GetAbilityName();
}