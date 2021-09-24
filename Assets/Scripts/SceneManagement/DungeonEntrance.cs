using RPG.Interaction;
using UnityEngine.SceneManagement;
using RPG.Dungeon;
using UnityEngine;
using System.Collections;
using System;

namespace RPG.SceneManagement
{
  public class DungeonEntrance : Interactable
  {
    public override void Interact(GameObject interacter)
    {
      //play door animation
      //show selection UI
      //teltport to selected dungeon level
      StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);
      yield return SceneManager.LoadSceneAsync("Dungeon");



      Destroy(transform.parent.gameObject);
    }
  }
}
