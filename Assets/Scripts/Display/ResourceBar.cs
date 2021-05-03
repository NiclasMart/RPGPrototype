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

    private void Update()
    {
      if (connectedValue != null) UpdateBar();
    }

    public virtual void UpdateBar()
    {
      SetFill(connectedValue.GetCurrentValue());
      SetMaxValue(connectedValue.GetMaxValue());
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