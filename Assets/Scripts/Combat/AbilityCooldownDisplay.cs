using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Combat
{
  public class AbilityCooldownDisplay : CooldownDisplay
  {
    public int index;
    public KeyCode activationKey;
    [SerializeField] Image iconDisplay;
    [SerializeField] TextMeshProUGUI usesCountDisplay;
    [HideInInspector] public Ability ability;
    [HideInInspector] public float nextCastTime = 0;
    CharacterStats stats;
    float cooldown = 0;
    bool active = true;

    public void Initialize(CharacterStats stats)
    {
      this.stats = stats;
    }

    private void Update()
    {
      if (ability && active) UpdateCooldown(nextCastTime - cooldown, cooldown);
    }

    public void SetAbility(Ability newAbility)
    {
      SetIcon(newAbility);
      if (ability != null) Destroy(ability.gameObject);

      if (newAbility == null) UpdateCooldown(Time.time, 1);
      nextCastTime = Mathf.NegativeInfinity;
      ability = newAbility;

      if (newAbility && newAbility.hasUses) usesCountDisplay.text = newAbility.useAmount.ToString();

    }

    public bool CooldownReady()
    {
      return nextCastTime < Time.time;
    }

    public void SetCooldown()
    {
      if (ability.hasUses)
      {
        usesCountDisplay.text = (ability.remainingUses - 1).ToString();

        if (ability.remainingUses == 1)
        {
          active = false;
          nextCastTime = Mathf.Infinity;
          UpdateCooldown(Time.time, 1);
          return;
        }
      }
      float cooldownReduction = Mathf.Min(0.75f, stats.GetStat(Stat.CooldownReduction) / 100f);
      cooldown = ability.cooldown - ability.cooldown * cooldownReduction;
      Debug.Log("cooldown " + cooldown);
      nextCastTime = Time.time + cooldown;
    }

    public void UpdateUsesDisplay()
    {
      usesCountDisplay.text = ability.remainingUses.ToString();
    }

    private void SetIcon(Ability newAbility)
    {
      if (newAbility == null) iconDisplay.sprite = null;
      else iconDisplay.sprite = newAbility.icon;
    }

  }
}
