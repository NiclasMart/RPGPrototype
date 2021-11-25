using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
  public class AbilityCooldownDisplay : CooldownDisplay
  {
    public int index;
    public KeyCode activationKey;
    [SerializeField] Image iconDisplay;
    [HideInInspector] public Ability ability;
    [HideInInspector] public float nextCastTime = 0;
    CharacterStats stats;
    float cooldown = 0;

    public void Initialize(CharacterStats stats)
    {
      this.stats = stats;
    }

    private void Update()
    {
      if (ability) UpdateCooldown(nextCastTime - cooldown, cooldown);
    }

    public void SetAbility(Ability newAbility)
    {
      SetIcon(newAbility);

      if (newAbility == null) UpdateCooldown(Time.time, 1);
      nextCastTime = Mathf.NegativeInfinity;
      ability = newAbility;
    }

    public bool CooldownReady()
    {
      return nextCastTime < Time.time;
    }

    public void SetCooldown()
    {
      float cooldownReduction = Mathf.Min(0.75f, stats.GetStat(Stat.CooldownReduction) / 100f);
      cooldown = ability.cooldown - ability.cooldown * cooldownReduction;
      Debug.Log("cooldown " + cooldown);
      nextCastTime = Time.time + cooldown;
    }

    private void SetIcon(Ability newAbility)
    {
      if (newAbility == null) iconDisplay.sprite = null;
      else iconDisplay.sprite = newAbility.icon;
    }

  }
}
