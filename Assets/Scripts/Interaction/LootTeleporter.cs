using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootTeleporter : Interactable
  {
    [SerializeField] Inventory connectedReciever;
    public override void Interact(GameObject interacter)
    {
      Inventory playerInventory = interacter.GetComponent<Interacter>().inventory;
      connectedReciever.DeleteAllItems();
      connectedReciever.AddItems(playerInventory.GetItemList());
      playerInventory.DeleteAllItems();
    }
  }
}
