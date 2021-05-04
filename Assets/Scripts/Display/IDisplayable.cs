using UnityEngine.Events;

namespace RPG.Display
{
  [System.Serializable]
  public class ValueChangeEvent : UnityEvent<IDisplayable>
  {
  }
  public interface IDisplayable
  {
    float GetCurrentValue();
    float GetMaxValue();
  }
}