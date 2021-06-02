using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ItemDisplaySlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;
    [SerializeField] Button selectable;
    [HideInInspector] public string slottedItemID { get; private set; }
    InventoryDisplay inventory;
    Color stdColor;

    private void Awake()
    {
      stdColor = selectable.GetComponent<Image>().color;
    }

    public void Initialize(Sprite sprite, string itemID, InventoryDisplay inventory)
    {
      SetIcon(sprite);
      slottedItemID = itemID;
      this.inventory = inventory;
    }

    public void Select()
    {
      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public void Deselect()
    {
      SetColor(stdColor);
    }

    void SetIcon(Sprite icon)
    {
      iconSlot.sprite = icon;
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }

  }
}
