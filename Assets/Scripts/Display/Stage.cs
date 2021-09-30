using UnityEngine.UI;
using UnityEngine;
using System;

namespace RPG.Display
{
  public class Stage : MonoBehaviour
  {
    public int number;
    [SerializeField] Image disabler;
    [SerializeField] Color color;
    bool active, selected = false;

    public Action<Stage> onSelect;

    public void SetActiveState(bool state)
    {
      active = state;
      SetColor(color, active ? 0 : 0.5f);
    }

    public void Select()
    {
      if (!active || selected) return;
      selected = true;
      SetColor(Color.white, 0.4f);
      onSelect.Invoke(this);
    }

    public void Deselect()
    {
      SetColor(color, 0);
      selected = false;
    }

    void SetColor(Color color, float alpha)
    {
      disabler.color = new Color(color.r, color.g, color.b, alpha);
    }
  }
}
