using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootTeleporter : Interactable
  {
    [SerializeField] LootBank connectedBank;
    public override void Interact(GameObject interacter)
    {
      TeleportItems(interacter);
    }

    public void TeleportItems(GameObject interacter)
    {
      SimpleInventory playerInventory = interacter.GetComponent<Interacter>().inventory;
      interacter.GetComponent<SoulEnergy>().Reset();
      connectedBank.AddLoot(playerInventory.GetItemList());
      playerInventory.DeleteAllItems();
    }
  }
}
