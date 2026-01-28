using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("Drag the object with the skill/movement script here")]
    public GameObject targetObject; 
    
    [Tooltip("The Image component that will drain (Set Image Type to Filled)")]
    public Image fillImage;

    private ICooldown targetCooldown;

    void Start()
    {
        // Find the interface on the target object
        if (targetObject != null)
        {
            targetCooldown = targetObject.GetComponent<ICooldown>();
        }

        if (targetCooldown == null)
        {
            Debug.LogError("UI Error: Target object does not implement ICooldown!");
            enabled = false; // Turn off script to save performance
        }
    }

    void Update()
    {
        if (targetCooldown != null)
        {
            // Simply ask the interface for the number!
            fillImage.fillAmount = targetCooldown.GetCooldownFactor();
        }
    }
}