using UnityEngine;

public class ThrowPotSkill : SkillBase
{
    public GameObject potPrefab;
    public Transform firePoint;

    // We only have to write what happens, not how inputs/cooldowns work!
    protected override void OnSkillTriggered()
    {
        Debug.Log("Throwing Pot!");
        Instantiate(potPrefab, firePoint.position, Quaternion.identity);
    }
}