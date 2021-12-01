using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Display
{
  public class StageSelector : MonoBehaviour
  {
    [SerializeField] List<Stage> stages = new List<Stage>();
    [SerializeField] InitialSoulEnergySetter soulEnergySetter;
    Stage selectedStage = null;
    public Action<int> enterDungeon;

    private void Start()
    {
      ProgressionData data = PlayerInfo.GetPlayer().GetComponent<ProgressionData>();
      foreach (Stage stage in stages)
      {
        stage.SetActiveState(stage.number <= data.ReachedStage);
        stage.onSelect += ChangeSelection;
      }
    }

    public void EnterSelectedDungeon()
    {
      if (!selectedStage) return;
      soulEnergySetter.SetSoulEnergy();
      enterDungeon.Invoke(selectedStage.number);
    }

    void ChangeSelection(Stage newSelected)
    {
      if (selectedStage) selectedStage.Deselect();
      selectedStage = newSelected;
    }

  }
}