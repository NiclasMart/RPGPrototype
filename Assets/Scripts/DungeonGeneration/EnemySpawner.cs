using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dungeon
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField, Range(0, 1)] float enemyDensity = 0.6f;
    [SerializeField] Vector2 groupSize;
    [SerializeField] GameObject enemy;
    Generator dungeonGenerator;
    private void Awake()
    {
      dungeonGenerator = GetComponent<Generator>();
      dungeonGenerator.finishedGeneration += Spawn;
    }

    void Spawn()
    {
      ShuffleList(dungeonGenerator.roomsGraph.nodes);
      int roomAmount = Mathf.FloorToInt(dungeonGenerator.roomsGraph.nodes.Count * enemyDensity);

      SpawnEnemyGroups(roomAmount);
    }

    private void SpawnEnemyGroups(int roomAmount)
    {
      GameObject enemyHolder = new GameObject("Enemies");
      for (int i = 0; i < roomAmount; i++)
      {
        Room room = dungeonGenerator.roomsGraph.nodes[i];
        if (room == dungeonGenerator.startRoom || room == dungeonGenerator.endRoom) continue;
        GameObject go = Instantiate(enemy, room.GetCenterWorld() * dungeonGenerator.tileSize, Quaternion.identity, enemyHolder.transform);
        StartCoroutine(TestEnableOutline(go));

      }
    }

    IEnumerator TestEnableOutline(GameObject go)
    {
      yield return new WaitForEndOfFrame();
      go.GetComponent<Outline>().Initialize();
    }

    private static System.Random rng = new System.Random();
    void ShuffleList(List<Room> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        Room node = list[k];
        list[k] = list[n];
        list[n] = node;
      }
    }
  }
}
