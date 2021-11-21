using System.Collections;
using System.Collections.Generic;
using RPG.Interaction;
using RPG.Saving;
using UnityEngine;
using TMPro;
using System;
using RPG.Core;

namespace RPG.Stats
{
  public class InitialSoulEnergySetter : MonoBehaviour, ISaveable
  {
    [SerializeField] int baseIncresement = 50, baseCost = 50;
    [SerializeField] TextMeshProUGUI valueDisplay;
    [SerializeField] PlayerInventory inventory;
    int initialSoulEnergy = 0;

    public int InitialSoulEnergy
    {
      get { return initialSoulEnergy; }
      private set
      {
        initialSoulEnergy = value;
        UpdateUI();
      }
    }

    public void IncreaseInitialValue()
    {
      int gems = inventory.GetGems(baseCost);
      if (gems == 0) return;

      InitialSoulEnergy += baseIncresement;
    }

    public void SetSoulEnergy()
    {
      PlayerInfo.GetPlayer().GetComponent<SoulEnergy>().SetInitialSouls(initialSoulEnergy);
      InitialSoulEnergy = 0;
    }

    private void UpdateUI()
    {
      valueDisplay.text = InitialSoulEnergy.ToString();
    }

    public object CaptureSaveData(SaveType saveType)
    {
      return InitialSoulEnergy;
    }

    public void RestoreSaveData(object data)
    {
      InitialSoulEnergy = (int)data;
    }
  }
}
