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
      connectedInventory.gameObject.SetActive(true);
    }

    public override void Deselect()
    {
      base.Deselect();
      connectedInventory.gameObject.SetActive(false);
    }
  }
}
