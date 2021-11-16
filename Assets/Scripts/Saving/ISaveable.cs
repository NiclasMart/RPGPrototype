using System;

namespace RPG.Saving
{
  [Serializable]
  public enum SaveType
  {
    All,
    PlayerSpecific
  }
  
  interface ISaveable
  {
    object CaptureSaveData(SaveType saveType);
    void RestoreSaveData(object data);
  }
}