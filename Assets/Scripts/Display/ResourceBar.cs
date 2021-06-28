using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ResourceBar : UIElement
  {
    [SerializeField] protected Image fill;
    [SerializeField] protected TextMeshProUGUI currentValueDisplay;
    [SerializeField] protected TextMeshProUGUI maxValueDisplay;

    public override void UpdateUI(IDisplayable value)
    {
      base.UpdateUI(value);
      if (value == null) return;
      float normalizedValue = NormalizeValue(value);
      SetTextDisplay(value);
      SetFill(normalizedValue);
    }

    protected void SetFill(float value)
    {
      fill.fillAmount = value;
    }

    protected void SetTextDisplay(IDisplayable value)
    {
      if (maxValueDisplay) maxValueDisplay.text = FormatValue(value.GetMaxValue());
      if (currentValueDisplay) currentValueDisplay.text = FormatValue(value.GetCurrentValue());
    }

    private float NormalizeValue(IDisplayable value)
    {
      return Mathf.InverseLerp(0, value.GetMaxValue(), value.GetCurrentValue());
    }

    private string FormatValue(float value)
    {
      return Mathf.Floor(value).ToString();
    }
  }
}