using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
  public class CooldownDisplay : MonoBehaviour
  {
    [SerializeField] protected Image cooldownDisplay;

    public void UpdateCooldown(float lastActivationTime, float cooldown)
    {
      float remainingTime = Mathf.Max(0, cooldown - (Time.time - lastActivationTime));
      cooldownDisplay.fillAmount = remainingTime / cooldown;
    }
  }
}
