using System;
using TMPro;
using UnityEngine;

namespace RPG.Display
{
  public class ValueDisplay : UIElement
  {
    [SerializeField] TextMeshProUGUI valueDisplay;
    private void Update()
    {
      if (connectedValue != null) UpdateValue();
    }

    private void UpdateValue()
    {
      valueDisplay.text = connectedValue.GetCurrentValue().ToString();
    }
  }
}