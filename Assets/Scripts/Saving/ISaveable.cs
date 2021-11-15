namespace RPG.Saving
{
  public enum SaveType
  {
    Always,
    Transition
  }
  
  interface ISaveable
  {
    object CaptureSaveData(SaveType saveType);
    void RestoreSaveData(object data);
  }
}