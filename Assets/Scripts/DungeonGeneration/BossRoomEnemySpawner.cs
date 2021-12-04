using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Stats;
using UnityEngine;

namespace RPG.Dungeon
{
  public class BossRoomEnemySpawner : MonoBehaviour
  {
    [SerializeField] float spawnInterval;
    [SerializeField] int maxEnemies;
    [SerializeField] Vector2Int waveSize;
    [SerializeField] List<GameObject> enemySpawnList;
    [SerializeField] GameObject enemyHolder;

    [HideInInspector] public bool active = true;

    private void Start()
    {
      StartCoroutine(Spawning());
    }

    private void Spawn()
    {
      int spawnEnemyCount = Random.Range(waveSize.x, waveSize.y + 1);

      for (int i = 0; i < spawnEnemyCount; i++)
      {
        if (enemyHolder.transform.childCount >= maxEnemies) return;

        GameObject enemy = enemySpawnList[Random.Range(0, enemySpawnList.Count)];
        enemy.GetComponent<CharacterStats>().SetLevel(50);
        Instantiate(enemy, transform.position, Quaternion.identity, enemyHolder.transform);
      }

    }

    private void ClearDeadEnemy()
    {
      for (int i = enemyHolder.transform.childCount; i > 0; i--)
      {
        Health health = enemyHolder.transform.GetChild(i - 1).GetComponent<Health>();
        if (health && health.IsDead) Destroy(health.gameObject);
      }
    }

    IEnumerator Spawning()
    {
      yield return new WaitForSeconds(Random.Range(0, 3f));
      while (active)
      {
        ClearDeadEnemy();
        Spawn();

        yield return new WaitForSeconds(spawnInterval);
      }

    }
  }
}
