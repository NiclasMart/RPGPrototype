using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class InventoryDisplay : MonoBehaviour
  {
    [SerializeField] ItemDisplaySlot itemSlot;
    [SerializeField] RectTransform list;
    [SerializeField] Text capacityDisplay;

    public void AddNewItemToDisplay(Sprite sprite)
    {
      ItemDisplaySlot slot = Instantiate(itemSlot, list);
      slot.SetIcon(sprite);
    }

    public void UpdateCapacityDisplay(float currentValue, float maxValue)
    {
      string value = string.Concat(currentValue + "/" + maxValue);
      capacityDisplay.text = value;
    }
  }
}