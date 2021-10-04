using System;
using System.Collections;
using RPG.Core;
using RPG.Dungeon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] float activationLightIntensifier;
    [SerializeField] int teleportTime = 3;
    [SerializeField] DungeonGenerationData dungeonData;
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
      StartCoroutine(Teleporting());
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject != PlayerInfo.GetPlayer()) return;
      lightComponent.range = defaultLightRange;
      StopCoroutine(Teleporting());
      teleportActive = false;
    }

    IEnumerator Teleporting()
    {
      float activeTime = 0;
      while (activeTime < teleportTime)
      {
        lightComponent.range += activationLightIntensifier;
        yield return new WaitForSeconds(0.1f);
        activeTime += 0.1f;
      }

      yield return Teleport();
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);
      dungeonData.CompletedCurrentDepthLevel();
      yield return SceneManager.LoadSceneAsync("Dungeon_Stage" + dungeonData.currentStage);

      Destroy(transform.parent.gameObject);
    }
  }
}