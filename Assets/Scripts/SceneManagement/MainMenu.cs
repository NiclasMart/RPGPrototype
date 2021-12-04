using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class MainMenu : MonoBehaviour
  {
    [SerializeField] GameObject warningText, controllPanel;
    [SerializeField] SavingSystem saveSystem;

    public void Save()
    {
      if (SceneManager.GetActiveScene().buildIndex != 0)
      {
        StartCoroutine(ShowingSaveWarning());
        return;
      }
      saveSystem.Save("PlayerData", SaveType.All);
    }

    public void ToggleControllPanel(bool on)
    {
      controllPanel.SetActive(on);
    }

    public void Exit()
    {
      Application.Quit();
    }

    IEnumerator ShowingSaveWarning()
    {
      warningText.SetActive(true);

      yield return new WaitForSeconds(3f);

      warningText.SetActive(false);
    }
  }
}
