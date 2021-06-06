using RPG.Display;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class ItemSlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;
    [SerializeField] Button selectable;
    [HideInInspector] public Item item { get; private set; }
    Inventory inventory;
    Color stdColor;

    private void Awake()
    {
      stdColor = selectable.GetComponent<Image>().color;
    }

    public void Initialize(Item item, Inventory inventory)
    {
      SetIcon(item);
      this.item = item;
      this.inventory = inventory;
    }

    public virtual void Select()
    {
      if (inventory.selectedSlot == this) return;
      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public virtual void Deselect()
    {
      SetColor(stdColor);
    }

    void SetIcon(Item item)
    {
      if (!item) return;
      iconSlot.sprite = item.icon;
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }

  }
}
