using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class StoryScreenDisplay : MonoBehaviour
{
  [SerializeField] string saveFileName;
  [SerializeField] bool hasTriggert = false;
  [SerializeField] protected CanvasGroup endScreen;
  [SerializeField] bool triggerOnStart = false;
  [SerializeField] float displayTime;

  private void Awake() 
  {
    object data = GetComponent<SavingSystem>().LoadDataOfSingleObject(saveFileName);
    if (data != null) hasTriggert = (bool)data;
  }

  private void Start()
  {
    if (triggerOnStart) DisplayStoryScreen();
    
  }

  public void DisableStoryScreen()
  {
    endScreen.gameObject.SetActive(false);
  }

  public void DisplayStoryScreen()
  {
    if (hasTriggert) return;
    hasTriggert = true;
    GetComponent<SavingSystem>().SaveDataOfSingleObject(hasTriggert, saveFileName);
    StartCoroutine(ShowingEndscreen());
  }

  protected IEnumerator ShowingEndscreen()
  {
    endScreen.gameObject.SetActive(true);

    while (endScreen.alpha <= 1)
    {
      endScreen.alpha += 0.01f;
      yield return new WaitForSeconds(displayTime / 100f);
    }
  }
}
