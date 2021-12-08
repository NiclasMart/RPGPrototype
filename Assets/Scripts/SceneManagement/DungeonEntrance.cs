using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using RPG.Display;
using RPG.Core;
using RPG.Stats;
using RPG.Saving;
using RPG.Items;

namespace RPG.Interaction
{
  public class DungeonEntrance : MonoBehaviour
  {
    [SerializeField] StageSelector selector;
    [SerializeField] DungeonGenerationData data;
    [SerializeField] LootBank lootBank;

    private void Awake()
    {
      selector.enterDungeon += EnterDungeon;
    }

    void EnterDungeon(int stage)
    {
      Debug.Log("Dungeon Stage: " + stage);
      PrepareTeleport(stage);
      StartCoroutine(Teleport());
    }

    private void PrepareTeleport(int stage)
    {
      data.currentStage = stage;
      data.currentDepth = 1;
      lootBank.ClearLoot();
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);

      FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.All);
      yield return SceneManager.LoadSceneAsync("Dungeon_Stage" + data.currentStage);

      Destroy(transform.parent.gameObject);
    }
  }
}
