using UnityEngine;

namespace RPG.Interaction
{
  public abstract class Inventory : MonoBehaviour 
  { 
    public abstract void SelectSlot(ItemSlot slot);
  }
}