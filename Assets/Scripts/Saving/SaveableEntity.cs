using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace RPG.Saving
{
  [ExecuteAlways]
  public class SaveableEntity : MonoBehaviour
  {
    [SerializeField] string uniqueIdentifier = "";

    public string GUID => uniqueIdentifier;

    public object CaptureSaveData(SaveType saveType)
    {
      Dictionary<string, object> state = new Dictionary<string, object>();
      foreach (ISaveable saveable in GetComponents<ISaveable>())
      {
        object data = saveable.CaptureSaveData(saveType);
        if (data != null) state[saveable.GetType().ToString()] = data;
      }
      return state;
    }

    public void RestoreSaveData(object data)
    {
      Dictionary<string, object> state = (Dictionary<string, object>)data;

      foreach (ISaveable saveable in GetComponents<ISaveable>())
      {
        if (!state.ContainsKey(saveable.GetType().ToString())) continue;
        saveable.RestoreSaveData(state[saveable.GetType().ToString()]);
      }
    }

    private void Update()
    {
#if UNITY_EDITOR      
      if (Application.IsPlaying(this)) return;
      if (string.IsNullOrEmpty(gameObject.scene.path)) return;  //checks if gameObject is within a prefab szene

      SerializedObject serializedObject = new SerializedObject(this);
      SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

      if (string.IsNullOrEmpty(property.stringValue))
      {
        property.stringValue = System.Guid.NewGuid().ToString();
        serializedObject.ApplyModifiedProperties();
      }
#endif
    }  
  }
}