using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using RPG.Saving;
using UnityEngine;

namespace RPG.Interaction
{
  public class LootShrine : Interactable, ISaveable
  {
    [SerializeField] Gradient colorSchema = new Gradient();
    [SerializeField] LootBank connectedBank;
    Light showLight;

    private void Awake()
    {
      showLight = GetComponentInChildren<Light>();
      float capacityLevel = connectedBank.GetCapacityLevel();
      showLight.color = colorSchema.Evaluate(capacityLevel);
      showLight.enabled = !connectedBank.Empty;
    }

    public override void Interact(GameObject interacter)
    {
      PlayerInventory playerInventory = interacter.GetComponent<Interacter>().mainInventory;
      playerInventory.AddItems(connectedBank.GetLoot());
      FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.PlayerSpecific);
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
    }
  }
}
