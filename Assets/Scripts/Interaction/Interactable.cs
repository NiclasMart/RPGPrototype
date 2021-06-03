using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Interaction
{
  public class Interactable : MonoBehaviour, IInteraction
  {
    public GameObject GetGameObject()
    {
      return gameObject;
    }

    public virtual void Interact(GameObject interacter) { }
  }
}
