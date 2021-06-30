using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
  public class AttackColdownDisplay : CooldownDisplay
  {
    private void Update()
    {
      transform.position = Input.mousePosition;
      ToggleDisplay(PlayerInfo.GetPlayerCursor().currentState == PlayerCursor.CursorType.STANDARD);
    }

    private void ToggleDisplay(bool active)
    {
      GetComponent<Image>().enabled = active;
      cooldownDisplay.enabled = active;
    }
  }
}
