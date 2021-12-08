using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using RPG.Saving;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootShrine : Interactable, ISaveable
  {
    [SerializeField] LootBank connectedBank;
    Light showLight;

    private void Awake()
    {
      SetLight();
    }

    public override void Interact(GameObject interacter)
    {
      PlayerInventory playerInventory = interacter.GetComponent<Interacter>().mainInventory;
      playerInventory.AddItems(connectedBank.GetLoot());
      SetLight();
    }

    public object CaptureSaveData(SaveType saveType)
    {
      List<object> saveData = new List<object>();
      foreach (var item in connectedBank.ShowLoot())
      {
        saveData.Add(item.GetSaveData());
      }
      return saveData;
    }

    public void RestoreSaveData(object data)
    {
      connectedBank.ClearLoot();

      List<object> saveData = (List<object>)data;
      foreach (object obj in saveData)
      {
        connectedBank.AddLoot((obj as Item.SaveData).CreateItemFromData());
      }

      SetLight();
    }

    private void SetLight()
    {
      showLight = GetComponentInChildren<Light>();
      showLight.enabled = !connectedBank.Empty;
    }
  }
}
