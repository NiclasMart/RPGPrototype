using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Dungeon;
using RPG.Stats;
using UnityEngine;

public class KillHandler : StoryScreenDisplay
{

  [SerializeField] GameObject enemyHolder, exitPortal;

  public void HandleDeath()
  {
    PlayerInfo.GetPlayer().GetComponent<Health>().HealPercentageMax(1);

    DisableenemySpawner();
    KillAllEnemies();
    EndableExitPortal();

    DisplayStoryScreen();
  }

  private void EndableExitPortal()
  {
    exitPortal.SetActive(true);
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
}
