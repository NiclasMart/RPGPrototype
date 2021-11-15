using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.Saving
{
  public class SavingSystem : MonoBehaviour
  {
    public void Save(string saveFile, SaveType saveType)
    {
      Dictionary<string, object> data = new Dictionary<string, object>();
      CaptureSaveData(data, saveType);
      SaveFile(saveFile, data);
    }

    public void Load(string saveFile)
    {
      Dictionary<string, object> data = LoadFile(saveFile);
      RestoreSaveData(data);
    }

    private string GetPathFromSaveFile(string saveFile)
    {
      return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }

    private void SaveFile(string saveFile, object state)
    {
      string path = GetPathFromSaveFile(saveFile);
      Debug.Log("Saving to " + path);

      using (FileStream stream = File.Open(path, FileMode.Create))
      {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, state);
      }
    }

    private Dictionary<string, object> LoadFile(string saveFile)
    {
      string path = GetPathFromSaveFile(saveFile);
      Debug.Log("Loading from " + path);
      if (!File.Exists(path))
      {
        return new Dictionary<string, object>();
      }
      using (FileStream stream = File.Open(path, FileMode.Open))
      {
        BinaryFormatter formatter = new BinaryFormatter();
        return (Dictionary<string, object>)formatter.Deserialize(stream);
      }
    }

    private void CaptureSaveData(Dictionary<string, object> data, SaveType saveType)
    {
      foreach (SaveableEntity entity in FindObjectsOfType<SaveableEntity>())
      {
        data[entity.GUID] = entity.CaptureSaveData(saveType);
      }
    }

    private void RestoreSaveData(Dictionary<string, object> data)
    {
      foreach (SaveableEntity entity in FindObjectsOfType<SaveableEntity>())
      {
        if (!data.ContainsKey(entity.GUID)) continue;
        entity.RestoreSaveData(data[entity.GUID]);
      }
    }
  }
}
