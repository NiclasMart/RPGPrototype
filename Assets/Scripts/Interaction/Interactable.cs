using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using static RPG.Core.PlayerCursor;

namespace RPG.Interaction
{
  public class Interactable : MonoBehaviour, IInteraction
  {
    public CursorType interactionType;
    
    public GameObject GetGameObject()
    {
      return gameObject;
    }

    public virtual void Interact(GameObject interacter) { }
  }
}
