using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class EquipmentSlot : ItemSlot
  {
    public SimpleInventory connectedInventory;
    GearChanger gearChanger;

    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(Select);
      gearChanger = FindObjectOfType<GearChanger>();
    }

    public override void Select()
    {
      base.Select();
      connectedInventory.onSecondClick += SetGear;
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
      connectedInventory.onSecondClick -= SetGear;
      connectedInventory.gameObject.SetActive(false);
    }

    public void SetGear(Item item)
    {
      SetIcon(item);
      gearChanger.EquipGear(item);
    }
  }
}
