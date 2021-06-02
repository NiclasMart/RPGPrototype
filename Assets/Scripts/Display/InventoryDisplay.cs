using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class InventoryDisplay : MonoBehaviour
  {
    [SerializeField] ItemDisplaySlot itemSlot;
    [SerializeField] RectTransform list;
    [SerializeField] TextMeshProUGUI capacityDisplay;
    ItemDisplaySlot currentlySelectedSlot = null;

    public void AddNewItemToDisplay(Sprite sprite)
    {
      ItemDisplaySlot slot = Instantiate(itemSlot, list);
      slot.Initialize(sprite, this);
    }

    public void UpdateCapacityDisplay(float currentValue, float maxValue)
    {
      string value = string.Concat(currentValue + "/" + maxValue);
      capacityDisplay.text = value;
    }

    public void SelectSlot(ItemDisplaySlot slot)
    {
      if (currentlySelectedSlot) currentlySelectedSlot.Deselect();
      currentlySelectedSlot = slot;
    }

    public void DeleteSelectedItem()
    {
      if (!currentlySelectedSlot) return;
      Destroy(currentlySelectedSlot.gameObject);
    }
  }
}