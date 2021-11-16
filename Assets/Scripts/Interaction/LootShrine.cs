using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootShrine : Interactable
  {
    [SerializeField] LootBank connectedBank;
    Light showLight;

    private void Awake() 
    {
      showLight = GetComponentInChildren<Light>();  
      showLight.enabled = !connectedBank.Empty;
    }

    public override void Interact(GameObject interacter)
    {
      PlayerInventory playerInventory = interacter.GetComponent<Interacter>().mainInventory;
      playerInventory.AddItems(connectedBank.GetLoot());
    }
  }
}
