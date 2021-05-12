using UnityEngine;

namespace RPG.Display
{
  public class OverheadDisplay : ResourceBar
  {
    Canvas canvas;

    private void Awake()
    {
      canvas = transform.parent.GetComponent<Canvas>();
    }

    private void Start()
    {
      canvas.enabled = false;
    }

    public void Show(IDisplayable value)
    {
      base.UpdateUI(value);
      if (value.GetCurrentValue() < value.GetMaxValue()) canvas.enabled = true;
      if (value.GetCurrentValue() <= 0) canvas.enabled = false;
    }
  }
}