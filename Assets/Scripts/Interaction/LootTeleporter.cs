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
      TeleportAllItems(interacter);
    }

    public void TeleportAllItems(GameObject interacter)
    {
      SimpleInventory playerInventory = interacter.GetComponent<Interacter>().inventory;
      connectedBank.AddLoot(playerInventory.GetItemList());
      playerInventory.DeleteAllItems();
    }

    //n -> every n'th item is teleportet
    public void TeleportPortionOfItems(GameObject interacter, float n)
    {
      SimpleInventory playerInventory = interacter.GetComponent<Interacter>().inventory;
      List<Item> savedLoot = new List<Item>();
      int count = 0;
      foreach (var item in playerInventory.GetItemList())
      {
        count++;
        if (count % n == 0) savedLoot.Add(item);
      }
      
      connectedBank.AddLoot(savedLoot);
      playerInventory.DeleteAllItems();
    }
  }
}
