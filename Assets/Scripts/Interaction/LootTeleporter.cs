using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootTeleporter : Interactable
  {
    [SerializeField] SimpleInventory connectedReciever;
    public override void Interact(GameObject interacter)
    {
      SimpleInventory playerInventory = interacter.GetComponent<Interacter>().inventory;
      connectedReciever.DeleteAllItems();
      connectedReciever.AddItems(playerInventory.GetItemList());
      playerInventory.DeleteAllItems();
    }
  }
}
