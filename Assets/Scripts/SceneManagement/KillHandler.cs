using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Dungeon;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

public class KillHandler : MonoBehaviour, ISaveable
{
  [SerializeField] bool hasBeenKilled;
  [SerializeField] GameObject enemyHolder, exitPortal;
  [SerializeField] CanvasGroup endScreen;


  public void HandleDeath()
  {
    DisableenemySpawner();
    KillAllEnemies();
    EndableExitPortal();
    StartCoroutine(ShowingEndscreen());
  }

  private void EndableExitPortal()
  {
    exitPortal.SetActive(true);
  }

  public void DisableEndScreen()
  {
    endScreen.gameObject.SetActive(false);
  }



  private void KillAllEnemies()
  {
    foreach (Transform enemy in enemyHolder.transform)
    {
      Health health = enemy.GetComponent<Health>();
      if (health) health.ApplyDamage(null, 10000);
    }
  }

  private void DisableenemySpawner()
  {
    foreach (var spawner in FindObjectsOfType<BossRoomEnemySpawner>())
    {
      spawner.active = false;
    }
  }

  IEnumerator ShowingEndscreen()
  {
    endScreen.gameObject.SetActive(true);

    while (endScreen.alpha <= 1)
    {
      endScreen.alpha += 0.01f;
      yield return new WaitForSeconds(0.04f);
    }
  }

  public object CaptureSaveData(SaveType saveType)
  {
    return hasBeenKilled;
  }

  public void RestoreSaveData(object data)
  {
    hasBeenKilled = (bool)data;
  }
}
