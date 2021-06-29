using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
  public class AttackColdownDisplay : MonoBehaviour
  {
    [SerializeField] Image cooldownDisplay;

    private void Update()
    {
      transform.position = Input.mousePosition;
      ToggleDisplay(PlayerInfo.GetPlayerCursor().currentState == PlayerCursor.CursorType.STANDARD);
    }

    public void UpdateCooldown(float lastAttackTime, float attackSpeed)
    {
      float cooldownTime = 1f / attackSpeed;
      float remainingTime = Mathf.Max(0, cooldownTime - (Time.time - lastAttackTime));
      cooldownDisplay.fillAmount = 1 - remainingTime / cooldownTime;
    }

    private void ToggleDisplay(bool active)
    {
      GetComponent<Image>().enabled = active;
      cooldownDisplay.enabled = active;
    }
  }
}
