using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using RPG.Core;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.Stats
{
  public class SoulEnergy : MonoBehaviour, IDisplayable, ISaveable
  {
    [SerializeField] DungeonGenerationData stageData;
    int killAmount = 0, killsForMaxSoulEnergy;
    public ValueChangeEvent valueChange;

    public Action onGetEnergy;

    private void Awake()
    {
      killsForMaxSoulEnergy = (int)stageData.GetMaxSoulEnergyKills();
      valueChange.Invoke(this);
    }

    public void AddKill()
    {
      killAmount++;
      onGetEnergy?.Invoke();
      valueChange.Invoke(this);
    }

    public void AddEnergy()
    {
      killAmount++;
      valueChange.Invoke(this);
    }

    public void SetInitialSouls(int amount)
    {
      killAmount = amount;
      valueChange.Invoke(this);
    }

    public float GetSoulEnergyLevel()
    {
      return Mathf.Min(1, (float)killAmount / killsForMaxSoulEnergy);
    }

    public float GetCurrentValue()
    {
      return killAmount;
    }

    public float GetMaxValue()
    {
      return killsForMaxSoulEnergy;
    }

    public void Reset()
    {
      killAmount = 0;
      valueChange.Invoke(this);
    }

    public object CaptureSaveData(SaveType saveType)
    {
      if (saveType == SaveType.All) return killAmount;
      else return null;
    }

    public void RestoreSaveData(object data)
    {
      killAmount = (int)data;
      valueChange.Invoke(this);
    }
  }
}
