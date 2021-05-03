using UnityEngine;

namespace RPG.Display
{
  public class UIElement : MonoBehaviour
  {
    protected IDisplayable connectedValue = null;
    public virtual void ConnectElement(IDisplayable value)
    {
      connectedValue = value;
      SetVisible();
    }

    protected virtual void SetVisible()
    {
      gameObject.SetActive(connectedValue != null);
    }
  }
}