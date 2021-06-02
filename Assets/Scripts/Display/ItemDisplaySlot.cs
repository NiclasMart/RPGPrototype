using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ItemDisplaySlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;

    public void SetIcon(Sprite icon)
    {
      iconSlot.sprite = icon;
    }
  }
}