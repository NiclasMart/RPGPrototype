using RPG.Core;
using UnityEngine;


namespace RPG.Interaction
{
  public class Pickup : Interactable
  {
    public Item item;

    private void OnEnable()
    {
      item.CreateID();
    }

    public void Take()
    {
      //player.GetComponent<Fighter>().EquipWeapon(item);
      Destroy(gameObject);
      PlayerInfo.GetPlayerCursor().ResetTarget();
      print("resetTarget");
    }

    public override void Interact(GameObject interacter)
    {
      SimpleInventory inventory = interacter.GetComponent<Interacter>().inventory;
      if (!inventory) return;

      if (inventory.CheckCapacity(item.weight))
      {
        inventory.AddItem(item);
        Take();
      }
      else
      {
        //show full effect
      }
    }
  }
}
