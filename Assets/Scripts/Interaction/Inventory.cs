using System;
using UnityEngine;

namespace RPG.Interaction
{
  public abstract class Inventory : MonoBehaviour
  {
    [HideInInspector] public ItemSlot selectedSlot;
    public abstract void SelectSlot(ItemSlot slot);
  }
}