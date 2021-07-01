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
    }

    public override void Deselect()
    {
      border.color = borderDefaultColor;
      connectedInventory.gameObject.SetActive(false);
    }


  }
}
