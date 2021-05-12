using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ResourceBar : UIElement
  {
    [SerializeField] protected Slider slider;
    [SerializeField] protected Image fill;
    [SerializeField] protected TextMeshProUGUI currentValueDisplay;
    [SerializeField] protected TextMeshProUGUI maxValueDisplay;

    public override void UpdateUI(IDisplayable value)
    {
      base.UpdateUI(value);
      if (value == null) return;
      SetMaxValue(value.GetMaxValue());
      SetFill(value.GetCurrentValue());
    }

    protected void SetFill(float value)
    {
      slider.value = value;
      if (currentValueDisplay) currentValueDisplay.text = FormatValue(value);
    }

    protected void SetMaxValue(float value)
    {
      slider.maxValue = value;
      if (maxValueDisplay) maxValueDisplay.text = FormatValue(value);
    }

    private string FormatValue(float value)
    {
      return Mathf.Floor(value).ToString();
    }
  }
}