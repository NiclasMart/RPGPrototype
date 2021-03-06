using RPG.Display;
using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot
  {
    public ItemType equipmentType;
    public SimpleInventory connectedInventory;
    [SerializeField] Image border;
    [SerializeField] Color selectionColor;
    Color borderDefaultColor;
    protected PlayerInventory playerInventory;


    protected override void Awake()
    {
      base.Awake();
      borderDefaultColor = border.color;
      GetComponent<Button>().onClick.AddListener(Select);
      playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Select()
    {
      inventory.SelectSlot(this);
      border.color = selectionColor;
      connectedInventory.gameObject.SetActive(true);
      connectedInventory.onSecondClick += EquipItem;
    }

    public override void Deselect()
    {
      border.color = borderDefaultColor;
      connectedInventory.gameObject.SetActive(false);
      connectedInventory.onSecondClick -= EquipItem;
    }

    public virtual void EquipItem(Item item)
    {
      if (this.item != null) UnequipCurrentItem();
      connectedInventory.DeleteSelectedItem();

      SetIcon(item);
      this.item = item;
    }

    protected void UnequipCurrentItem()
    {
      connectedInventory.AddItem(item);
    }


  }
}
