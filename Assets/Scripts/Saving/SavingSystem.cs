using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RPG.Saving
{
  public class SavingSystem : MonoBehaviour
  {
    [SerializeField] bool debugMode = false;
    public void Save(string saveFile, SaveType saveType)
    {
      Dictionary<string, object> data = new Dictionary<string, object>();
      CaptureSaveData(data, saveType);
      SaveFile(saveFile, data);
    }

    public void Load(string saveFile)
    {
      if (debugMode) return;
      object data = LoadFile(saveFile);
      if (data != null && data is Dictionary<string, object>) RestoreSaveData((Dictionary<string, object>)data);
    }

    public void SaveDataOfSingleObject(object data, string fileName)
    {
      SaveFile(fileName, data);
    }

    public object LoadDataOfSingleObject(string fileName)
    {

      return LoadFile(fileName);
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

    private object LoadFile(string saveFile)
    {
      string path = GetPathFromSaveFile(saveFile);
      Debug.Log("Loading from " + path);
      if (!File.Exists(path))
      {
        return null;
      }
      using (FileStream stream = File.Open(path, FileMode.Open))
      {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter.Deserialize(stream);
      }
    }

    private void CaptureSaveData(Dictionary<string, object> data, SaveType saveType)
    {
      foreach (SaveableEntity entity in Object.FindObjectsOfType<SaveableEntity>(true))
      {
        data[entity.GUID] = entity.CaptureSaveData(saveType);
      }
    }

    private void RestoreSaveData(Dictionary<string, object> data)
    {
      foreach (SaveableEntity entity in Object.FindObjectsOfType<SaveableEntity>(true))
      {
        if (!data.ContainsKey(entity.GUID)) continue;
        entity.RestoreSaveData(data[entity.GUID]);
      }
    }
  }
}
