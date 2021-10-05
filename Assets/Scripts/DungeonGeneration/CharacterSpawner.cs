using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Dungeon
{
  public class CharacterSpawner : MonoBehaviour
  {
    [SerializeField] int totalEnemyAmount;
    [SerializeField] Vector2Int enemyGroupSize;
    [SerializeField] DungeonGenerationData stageData;
    Generator dungeonGenerator;
    List<GameObject> enemyList;
    List<int> levelList;

    private void Awake()
    {
      dungeonGenerator = GetComponent<Generator>();
      dungeonGenerator.finishedGeneration += Spawn;
      enemyList = stageData.GetEnemyList();
      levelList = stageData.GetLevelRange();
    }

    void Spawn()
    {
      SpawnPlayer();
      SpawnEnemyGroups();
    }

    private void SpawnPlayer()
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      Vector3 spawnPos = FindValidSpawnPosition(dungeonGenerator.startRoom);
      player.GetComponent<NavMeshAgent>().Warp(spawnPos);

      //place teleporter
      GameObject teleporter1 = Resources.Load("Prefabs/Portal1") as GameObject;
      Instantiate(teleporter1, spawnPos, Quaternion.identity);

      Vector3 endPos = FindValidSpawnPosition(dungeonGenerator.endRoom);
      GameObject teleporter2 = Resources.Load("Prefabs/Portal2") as GameObject;
      Instantiate(teleporter2, endPos, Quaternion.identity);

    }

    GameObject enemyHolder;
    private void SpawnEnemyGroups()
    {
      ShuffleList(dungeonGenerator.roomsGraph.nodes);

      enemyHolder = new GameObject("Enemies");
      int currentlySpawnedEnemys = 0;
      for (int i = 0; i < dungeonGenerator.roomsGraph.nodes.Count; i++)
      {
        Room room = dungeonGenerator.roomsGraph.nodes[i];
        if (room == dungeonGenerator.startRoom || room == dungeonGenerator.endRoom) continue;

        int groupSize = Random.Range(enemyGroupSize.x, enemyGroupSize.y + 1);
        currentlySpawnedEnemys += groupSize;

        SpawnGroupInRoom(room, groupSize);

        if (currentlySpawnedEnemys >= totalEnemyAmount) return;

      }
    }

    private void SpawnGroupInRoom(Room room, int groupSize)
    {
      for (int i = 0; i < groupSize; i++)
      {
        Vector3 spawnPos = FindValidSpawnPosition(room);
        GameObject enemy = GetEnemyFromSpawnList();
        enemy.GetComponent<CharacterStats>().SetLevel(levelList[Random.Range(0, levelList.Count)]);
        Instantiate(enemy, spawnPos, Quaternion.identity, enemyHolder.transform);
      }
    }

    private Vector3 FindValidSpawnPosition(Room room)
    {
      bool validPosition = true;
      int spawnOffsetY, spawnOffsetX;
      do
      {
        spawnOffsetX = Random.Range(0, room.size.x);
        spawnOffsetY = Random.Range(0, room.size.y);
        if (room is BluePrintRoom) validPosition = (room as BluePrintRoom).GetBlueprintPixel(spawnOffsetX, spawnOffsetY);
      } while (!validPosition);

      return new Vector3(room.position.y + spawnOffsetY, 0, room.position.x + spawnOffsetX) * dungeonGenerator.tileSize;
    }

    private GameObject GetEnemyFromSpawnList()
    {
      return enemyList[Random.Range(0, enemyList.Count)];
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
