using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class InventoryDisplay : MonoBehaviour
  {
    [SerializeField] KeyCode displayButton = KeyCode.I;
    [SerializeField] ItemDisplaySlot itemSlot;
    [SerializeField] RectTransform list;
    [SerializeField] TextMeshProUGUI capacityDisplay;
    [HideInInspector] public ItemDisplaySlot currentlySelectedSlot { get; private set; }

    bool displayActive = false;

    private void Awake()
    {
      transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
      ToggleDisplay();
    }

    public void AddNewItemToDisplay(Sprite sprite, string itemID)
    {
      ItemDisplaySlot slot = Instantiate(itemSlot, list);
      slot.Initialize(sprite, itemID, this);
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

    private void ToggleDisplay()
    {
      if (Input.GetKeyDown(displayButton))
      {
        displayActive = !displayActive;
        transform.GetChild(0).gameObject.SetActive(displayActive);
      }
    }
  }
}