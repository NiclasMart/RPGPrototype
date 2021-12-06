using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Dungeon
{
  public class DungeonDecorator : MonoBehaviour
  {
    Generator dungeonGenerator;
    [SerializeField] DungeonGenerationData data;
    [SerializeField] GameObject chestPrefab;

    private void Awake()
    {
      dungeonGenerator = GetComponent<Generator>();
      dungeonGenerator.finishedGeneration += Decorate;
    }

    private void Decorate()
    {
      SetLootChests();
    }

    private void SetLootChests()
    {
      int chestAmount = data.GetLootChestAmount();
      List<Room> deadEndRooms = new List<Room>();

      //find dead ends
      foreach (var room in dungeonGenerator.roomsGraph.nodes)
      {
        if (room == dungeonGenerator.startRoom || room == dungeonGenerator.endRoom) continue;
        if (room.connections.Count == 1) deadEndRooms.Add(room);
      }

      //if less dead ends than needed, find other rooms
      while (deadEndRooms.Count < chestAmount)
      {
        int index = UnityEngine.Random.Range(0, dungeonGenerator.roomsGraph.nodes.Count);
        deadEndRooms.Add(dungeonGenerator.roomsGraph.nodes[index]);
      }

      //get position in room and spawn chest
      int spawnedChests = 0;
      while (spawnedChests < chestAmount)
      {
        Room chestRoom = deadEndRooms[UnityEngine.Random.Range(0, deadEndRooms.Count)];
        Vector3 spawnPos = FindValidSpawnPosition(chestRoom);
        Quaternion quaternion = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 180), Vector3.up);
        Instantiate(chestPrefab, spawnPos, quaternion);

        spawnedChests++;
      }
    }

    private Vector3 FindValidSpawnPosition(Room room)
    {
      bool validPosition = true;
      int spawnOffsetY, spawnOffsetX;
      do
      {
        spawnOffsetX = UnityEngine.Random.Range(0, room.size.x);
        spawnOffsetY = UnityEngine.Random.Range(0, room.size.y);
        if (room is BluePrintRoom) validPosition = dungeonGenerator.CheckValidPosition(room, spawnOffsetX, spawnOffsetY);
      } while (!validPosition);

      return new Vector3(room.position.y + spawnOffsetY, 0, room.position.x + spawnOffsetX) * dungeonGenerator.tileSize;
    }
  }
}

