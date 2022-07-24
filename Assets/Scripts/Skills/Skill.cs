using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class Skill : ScriptableObject
{
    public string skillName;
    int maxCooldown;
    int currCooldown;
    public TargetMode targetMode;
    public Sprite icon;

    public void Activate(BattleManager BattleManager)
    {
        BattleManager.StartCoroutine(this.SkillEffect(BattleManager));
    }

    public virtual IEnumerator SkillEffect(BattleManager BattleManager)
    {
        Debug.Log("Uh oh!");
        yield break;
    }

    public virtual bool IsValid(List<BattleTile> targeted)
    {
        Debug.Log("YOU SHOULD NEVER SEE THIS. SKILL.VALIDATE RETURNS FALSE.");
        return false;
    }

    public virtual void Awake()
    {
        // Default values?
        targetMode = TargetMode.NONE;
    }

    void DecrementCooldown()
    {

    }
}
