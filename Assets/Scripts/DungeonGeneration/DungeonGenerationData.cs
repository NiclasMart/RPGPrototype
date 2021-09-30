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
    class GeneratorParameters
    {
      public int stageNumber;
      public List<Texture2D> DungeonBlueprints = new List<Texture2D>();
      public List<Texture2D> RoomBlueprints = new List<Texture2D>();
      public List<TileSetTable> tileSetTables = new List<TileSetTable>();
    }

    public int currentStage;
    [SerializeField] List<GeneratorParameters> stageData = new List<GeneratorParameters>();

  }
}
