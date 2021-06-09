using System;
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
    protected Inventory inventory;
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
      //handle second click
      if (inventory.selectedSlot == this)
      {
        SimpleInventory simpleInventory = inventory as SimpleInventory;
        if (simpleInventory) simpleInventory.onSecondClick.Invoke(inventory.selectedSlot.item);
        return;
      }

      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public virtual void Deselect()
    {
      SetColor(stdColor);
    }

    protected void SetIcon(Item item)
    {
      if (item == null) return;
      iconSlot.sprite = item.icon;
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }

  }
}
