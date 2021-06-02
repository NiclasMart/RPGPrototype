using UnityEngine;

namespace RPG.Display
{
  public class InventoryDisplay : MonoBehaviour
  {
    [SerializeField] ItemDisplaySlot itemSlot;
    [SerializeField] RectTransform list;

    public void AddNewItemToDisplay(Sprite sprite)
    {
      ItemDisplaySlot slot = Instantiate(itemSlot, list);
      slot.SetIcon(sprite);
    }
  }
}