using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Dungeon
{
  [CreateAssetMenuAttribute(fileName = "DungeonData", menuName = "New DungeonData")]
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
    }

    public int currentStage;
    public int currentDepth;
    [SerializeField] List<GeneratorParameters> stageData = new List<GeneratorParameters>();

    public List<GameObject> GetEnemyList()
    {
      //search for data within the list 
      GeneratorParameters stageParameters = null;
      foreach (var data in stageData)
      {
        if (data.stageNumber == currentStage)
        {
          stageParameters = data;
          break;
        }
      }

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

  }
}
