using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dungeon
{
  public class EnemySpawner : MonoBehaviour
  {
    [SerializeField] int totalEnemyAmount;
    [SerializeField] Vector2Int enemyGroupSize;
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
      SpawnEnemyGroups();
    }

    GameObject enemyHolder;
    private void SpawnEnemyGroups()
    {
      enemyHolder = new GameObject("Enemies");
      int currentlySpawnedEnemys = 0;
      for (int i = 0; i < dungeonGenerator.roomsGraph.nodes.Count; i++)
      {
        Room room = dungeonGenerator.roomsGraph.nodes[i];
        if (room == dungeonGenerator.startRoom || room == dungeonGenerator.endRoom) continue;

        int groupSize = Random.Range(enemyGroupSize.x, enemyGroupSize.y + 1);
        currentlySpawnedEnemys += groupSize;

        SpawnGroup(room, groupSize);
        
        if (currentlySpawnedEnemys >= totalEnemyAmount) return;

      }
    }

    private void SpawnGroup(Room room, int groupSize)
    {
      for (int i = 0; i < groupSize; i++)
      {
        bool validPosition = true;
        int spawnOffsetY, spawnOffsetX;
        do {
           spawnOffsetX = Random.Range(0, room.size.x);
           spawnOffsetY = Random.Range(0, room.size.y);
          if (room is BluePrintRoom) validPosition = (room as BluePrintRoom).GetBlueprintPixel(spawnOffsetX, spawnOffsetY);
        } while (!validPosition);

        Vector3 spawnPos = new Vector3(room.position.y + spawnOffsetY, 0, room.position.x + spawnOffsetX) * dungeonGenerator.tileSize;
        Instantiate(enemy, spawnPos, Quaternion.identity, enemyHolder.transform);
      }
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
