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
      if (Input.GetKeyDown(KeyCode.F5)) SaveData();
      if (Input.GetKeyDown(KeyCode.F6)) LoadData();
    }

    private void LoadData()
    {
      FindObjectOfType<SavingSystem>().Load("PlayerData");
    }

    private void SaveData()
    {
      FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.All);
    }
  }
}