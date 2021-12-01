using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Core
{
  [CreateAssetMenuAttribute(fileName = "DungeonData", menuName = "DungeonGeneration/New DungeonData")]
  public class DungeonGenerationData : ScriptableObject
  {
    [System.Serializable]
    class SpawnableEnemy
    {
      public GameObject enemy;
      public int frequency;
    }

    [System.Serializable]
    class GeneratorParameters
    {
      public int stageNumber;
      [Header("DungeonData")]
      public List<Texture2D> DungeonBlueprints = new List<Texture2D>();
      public List<Texture2D> RoomBlueprints = new List<Texture2D>();
      [Header("EnemyData")]
      public List<SpawnableEnemy> enemyPool = new List<SpawnableEnemy>();
      public Vector2Int levelRange;
      public int maxSoulEnergyKills;
    }

    public int currentStage;
    public int currentDepth;
    [SerializeField] List<GeneratorParameters> stageData = new List<GeneratorParameters>();

    float[] depthLevelMultiplicator = { 0.2f, 0.5f, 0.8f };

    public List<GameObject> GetEnemyList()
    {
      if (currentStage == 0) currentStage = 1;
      GeneratorParameters stageParameters = GetCurrentStagesData();

      //construct enemy list based on the specified data
      List<GameObject> enemyList = new List<GameObject>();
      foreach (var enemyData in stageParameters.enemyPool)
      {
        for (int i = 0; i < enemyData.frequency; i++)
        {
          enemyList.Add(enemyData.enemy);
        }
      }

      return enemyList;
    }

    public List<int> GetLevelRange()
    {
      List<int> levelSpan = new List<int>();
      GeneratorParameters stageParameters = GetCurrentStagesData();
      int levelCount = stageParameters.levelRange.y - stageParameters.levelRange.x + 1;
      for (int i = 0; i < levelCount; i++)
      {
        for (int j = 0; j < helpFunction(levelCount, i); j++)
        {
          levelSpan.Add(stageParameters.levelRange.x + i);
        }
      }

      return levelSpan;
    }

    public float GetMaxSoulEnergyKills()
    {
      return GetCurrentStagesData().maxSoulEnergyKills;
    }

    private int helpFunction(int levelCount, int i)
    {
      float x = Mathf.InverseLerp(1, levelCount, i + 1);
      float y = -25 * Mathf.Pow(x - depthLevelMultiplicator[currentDepth - 1], 2) + 10;
      if (y <= 1) y = 2;
      return Mathf.CeilToInt(y);
    } 

    private GeneratorParameters GetCurrentStagesData()
    {
      GeneratorParameters stageParameters = stageData[currentStage - 1];
      if (stageParameters.stageNumber == currentStage) return stageParameters;

      foreach (var data in stageData)
      {
        if (data.stageNumber == currentStage) return data;
      }
      return null;
    }

  }
}
