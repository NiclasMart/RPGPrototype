using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector] public float lastCastTime;

    private void Update()
    {
      if (ability) UpdateCooldown(lastCastTime, ability.cooldown);
    }

    public void SetAbility(Ability newAbility)
    {
      SetIcon(newAbility);

      lastCastTime = Mathf.NegativeInfinity;
      ability = newAbility;
    }

    public bool CooldownReady()
    {
      return lastCastTime + ability.cooldown < Time.time;
    }

    public void SetCooldown()
    {
      lastCastTime = Time.time;
    }

    private void SetIcon(Ability newAbility)
    {
      if (newAbility == null) iconDisplay.sprite = null;
      else iconDisplay.sprite = newAbility.icon;
    }

  }
}
