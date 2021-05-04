using System;
using TMPro;
using UnityEngine;

namespace RPG.Display
{
  public class ValueDisplay : UIElement
  {
    [SerializeField] TextMeshProUGUI valueDisplay;

    public override void UpdateUI(IDisplayable value)
    {
      base.UpdateUI(value);
      if (value == null) return;
      valueDisplay.text = value.GetCurrentValue().ToString();
    }
  }
}