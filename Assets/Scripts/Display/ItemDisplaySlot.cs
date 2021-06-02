using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ItemDisplaySlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;
    [SerializeField] Button selectable;
    InventoryDisplay inventory;
    Color stdColor;

    private void Awake()
    {
      stdColor = selectable.GetComponent<Image>().color;
    }

    public void Initialize(Sprite sprite, InventoryDisplay inventory)
    {
      SetIcon(sprite);
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
