using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class StageSelector : MonoBehaviour
  {
    [SerializeField] List<Stage> stages = new List<Stage>();
    [SerializeField] int highestStageReached = 1;

    Stage selectedStage = null;

    private void Awake()
    {
      foreach (Stage stage in stages)
      {
        stage.SetActiveState(stage.number <= highestStageReached);
        stage.onSelect += ChangeSelection;
      }
    }

    void ChangeSelection(Stage newSelected)
    {
      if (selectedStage) selectedStage.Deselect();
      selectedStage = newSelected;
    }

  }
}