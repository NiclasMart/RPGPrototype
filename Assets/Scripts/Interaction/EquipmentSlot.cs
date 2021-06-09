using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot
  {
    public SimpleInventory connectedInventory;

    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(Select);
    }

    public override void Select()
    {
      base.Select();
      connectedInventory.onSecondClick += EquipGear;
      connectedInventory.gameObject.SetActive(true);
    }

    //needed ?
    private bool AlreadySelected()
    {
      EquipmentSlot selectedSlot = inventory.selectedSlot as EquipmentSlot;
      if (!selectedSlot) return false;
      if (selectedSlot != this) return false;
      return true;
    }

    public override void Deselect()
    {
      base.Deselect();
      connectedInventory.onSecondClick -= EquipGear;
      connectedInventory.gameObject.SetActive(false);
    }

    public void EquipGear(GenericItem item)
    {
      SetIcon(item);
    }
  }
}
