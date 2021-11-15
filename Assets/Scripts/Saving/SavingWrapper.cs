using UnityEngine;

namespace RPG.Saving
{
  public class SavingWrapper : MonoBehaviour
  {
    const string saveFileName = "testSave";
    SavingSystem saveSystem;

    private void Awake()
    {
      saveSystem = GetComponent<SavingSystem>();
    }

    private void Update()
    {
      // if (Input.GetKeyDown(KeyCode.S)) SaveData();
      // if (Input.GetKeyDown(KeyCode.L)) LoadData();
    }

    private void LoadData()
    {
      saveSystem.Load(saveFileName);
    }

    private void SaveData()
    {
      saveSystem.Save(saveFileName, SaveType.Always);
    }
  }
}