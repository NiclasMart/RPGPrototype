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
    [HideInInspector] public ItemDisplaySlot currentlySelectedSlot { get; private set; }

    public void AddNewItemToDisplay(Sprite sprite, string itemID)
    {
      ItemDisplaySlot slot = Instantiate(itemSlot, list);
      slot.Initialize(sprite, itemID, this);
    }

    public void UpdateCapacityDisplay(float currentValue, float maxValue)
    {
      if (!capacityDisplay) return;
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

    public void Clear()
    {
      foreach (Transform slot in list.transform)
      {
        if (slot == transform) continue;
        Destroy(slot.gameObject);
      }
    }
  }
}