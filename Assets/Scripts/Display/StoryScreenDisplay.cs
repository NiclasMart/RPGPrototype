using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class StoryScreenDisplay : MonoBehaviour, ISaveable
{
  [SerializeField] bool hasTriggert = false;
  [SerializeField] protected CanvasGroup endScreen;
  [SerializeField] bool triggerOnStart = false;
  [SerializeField] float displayTime;

  private void Start()
  {
    Debug.Log("Show Screen");
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

  public object CaptureSaveData(SaveType saveType)
  {
    return hasTriggert;
  }

  public void RestoreSaveData(object data)
  {
    Debug.Log("Load screen");
    hasTriggert = (bool)data;
  }
}
