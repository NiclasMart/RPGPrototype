using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;

namespace RPG.Saving
{
  public class ProgressionData : MonoBehaviour, ISaveable
  {
    [SerializeField] bool debugMode;
    [SerializeField] DungeonGenerationData data;
    [SerializeField] int maxReachedStage = 1;
    public int ReachedStage => maxReachedStage;

    public void UpdateProgress()
    {
      if (data.currentDepth == 4)
      {
        data.currentStage++;
        data.currentDepth = 1;
        if (data.currentStage > maxReachedStage) maxReachedStage = data.currentStage;
      }
      else data.currentDepth++;
    }

    public object CaptureSaveData(SaveType saveType)
    {
      return maxReachedStage;
    }

    public void RestoreSaveData(object data)
    {
      if (debugMode) return;
      maxReachedStage = (int)data;
    }
  }
}
