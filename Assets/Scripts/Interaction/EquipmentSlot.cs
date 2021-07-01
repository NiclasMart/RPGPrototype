using RPG.Display;
using RPG.Items;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot
  {
    public ItemType equipmentType;
    public SimpleInventory connectedInventory;
    protected PlayerInventory playerInventory;
    

    protected override void Awake()
    {
      base.Awake();
      GetComponent<Button>().onClick.AddListener(Select);
      playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public override void Select()
    {
      inventory.SelectSlot(this);
      connectedInventory.gameObject.SetActive(true);
    }

    public override void Deselect()
    {
      connectedInventory.gameObject.SetActive(false);
    }

    
  }
}
