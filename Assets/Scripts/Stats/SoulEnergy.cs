using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using RPG.Core;
using UnityEngine;

namespace RPG.Stats
{
  public class SoulEnergy : MonoBehaviour, IDisplayable
  {
    [SerializeField] DungeonGenerationData stageData;
    int killAmount = 0, killsForMaxSoulEnergy;
    public ValueChangeEvent valueChange;

    private void Awake() 
    {
      killsForMaxSoulEnergy = (int)stageData.GetMaxSoulEnergyKills();
    }

    public void AddKill()
    {
      killAmount++;
      valueChange.Invoke(this);
    }

    public float GetSoulEnergyLevel()
    {
      return Mathf.Min(1, (float)killsForMaxSoulEnergy / killAmount);
    }

    public float GetCurrentValue()
    {
      return killAmount;
    }

    public float GetMaxValue()
    {
      return killsForMaxSoulEnergy; 
    }
  }
}
