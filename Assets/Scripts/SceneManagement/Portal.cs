using System;
using System.Collections;
using RPG.Core;
using RPG.Interaction;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] float activationLightIntensifier;
    [SerializeField] int teleportTime = 3;
    [SerializeField] DungeonGenerationData dungeonData;
    [SerializeField] bool homeTeleporter = false;
    Light lightComponent;
    float defaultLightRange;
    bool teleportActive = false;

    private void Awake()
    {
      lightComponent = GetComponentInChildren<Light>();
      defaultLightRange = lightComponent.range;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject != PlayerInfo.GetPlayer() || teleportActive) return;

      teleportActive = true;
      coroutine = StartCoroutine(Teleporting());
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject != PlayerInfo.GetPlayer() || !teleportActive) return;

      lightComponent.range = defaultLightRange;
      StopCoroutine(coroutine);
      teleportActive = false;
    }

    Coroutine coroutine = null;
    IEnumerator Teleporting()
    {
      float activeTime = 0;
      while (activeTime < teleportTime)
      {
        lightComponent.range += activationLightIntensifier;
        yield return new WaitForSeconds(0.1f);
        activeTime += 0.1f;
      }

      if (teleportActive) yield return Teleport();
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);

      if (homeTeleporter)
      {
        FindObjectOfType<LootTeleporter>().TeleportItems(PlayerInfo.GetPlayer());
        FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.PlayerSpecific);
        yield return SceneManager.LoadSceneAsync("Village");
      }
      else
      {
        PlayerInfo.GetPlayer().GetComponent<ProgressionData>().UpdateProgress();
        if (dungeonData.currentDepth == 4) PlayerInfo.GetPlayer().GetComponent<Health>().HealPercentageMax(1);
        FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.All);

        if (dungeonData.currentDepth == 4) yield return SceneManager.LoadSceneAsync("TransitionRoom");
        else yield return SceneManager.LoadSceneAsync("Dungeon_Stage" + dungeonData.currentStage);
      }

      Destroy(transform.parent.gameObject);
    }
  }
}