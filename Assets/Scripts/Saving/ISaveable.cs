namespace RPG.Saving
{
  interface ISaveable
  {
    object CaptureSaveData();
    void RestoreSaveData(object data);
  }
}