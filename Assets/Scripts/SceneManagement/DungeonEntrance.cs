using RPG.Interaction;
using UnityEngine.SceneManagement;
using RPG.Dungeon;
using UnityEngine;
using System.Collections;
using System;
using RPG.Display;

namespace RPG.SceneManagement
{
  public class DungeonEntrance : MonoBehaviour
  {
    [SerializeField] StageSelector selector;
    [SerializeField] DungeonGenerationData data;

    private void Awake()
    {
      selector.enterDungeon += EnterDungeon;
    }

    void EnterDungeon(int stage)
    {
      Debug.Log("Dungeon Stage: " + stage);
      PrepareDungeonData(stage);
      StartCoroutine(Teleport());
    }

    private void PrepareDungeonData(int stage)
    {
      data.currentStage = stage;
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);
      yield return SceneManager.LoadSceneAsync("Dungeon");

      Destroy(transform.parent.gameObject);
    }
  }
}
