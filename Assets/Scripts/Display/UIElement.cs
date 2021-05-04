using UnityEngine;

namespace RPG.Display
{
  public class UIElement : MonoBehaviour
  {
    public virtual void UpdateUI(IDisplayable value)
    {
      SetVisible(value);
    }

    public virtual void SetVisible(IDisplayable value)
    {
      gameObject.SetActive(value != null);
    }
  }
}