using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootTeleporter : Interactable
  {
    [SerializeField] Inventory connectedChest;
    public override void Interact(GameObject interacter)
    {
      Inventory playerInventory = interacter.GetComponent<Inventory>();
      connectedChest.AddItems(playerInventory.items);
      playerInventory.DeleteAllItems();
    }
  }
}
