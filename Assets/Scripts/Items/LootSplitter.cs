using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  public static class LootSplitter
  {
    static float gridCellSize = 0.5f, cellBorderSize = 0.05f;
    static HashSet<Vector3> lootGrid = new HashSet<Vector3>();

    public static Vector3 GetFreeGridPosition(Vector3 centerPoint)
    {
      Vector3 gridPosition = NormaizeToGrid(centerPoint);

      if (!lootGrid.Contains(gridPosition))
      {
        lootGrid.Add(gridPosition);
        return GenerateSpawnPoint(gridPosition, centerPoint.y);
      }

      float searchArea = 1f;
      Vector3 newGridPosition;
      do
      {
        int areaMultipier = Mathf.RoundToInt(searchArea);
        int xGridOffset = Random.Range(-areaMultipier, areaMultipier + 1);
        int zGridOffset = Random.Range(-areaMultipier, areaMultipier + 1);

        float newX = gridPosition.x + gridCellSize * xGridOffset;
        float newY = gridPosition.z + gridCellSize * zGridOffset;

        newGridPosition = new Vector3(newX, 0, newY);
        searchArea += 0.25f;
      } while (lootGrid.Contains(newGridPosition));

      lootGrid.Add(newGridPosition);
      return GenerateSpawnPoint(newGridPosition, centerPoint.y);
    }

    public static void FreeGridPosition(Vector3 position)
    {
      lootGrid.Remove(NormaizeToGrid(position));
    }

    static Vector3 NormaizeToGrid(Vector3 point)
    {
      float roundParameter = 1f / gridCellSize;
      point *= roundParameter;
      Vector3 roundPosition = new Vector3(Mathf.Round(point.x), 0, Mathf.Round(point.z));
      return roundPosition / roundParameter;
    }

    static Vector3 GenerateSpawnPoint(Vector3 pos, float yPos)
    {
      float validOffset = gridCellSize / 2f - cellBorderSize;
      return new Vector3(pos.x + Random.Range(-validOffset, validOffset), yPos, pos.z + Random.Range(-validOffset, validOffset));
    }
  }
}