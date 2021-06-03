using RPG.Core;
using RPG.Interaction;
using UnityEngine;

public class CombatTarget : MonoBehaviour, IInteractable
{
  public GameObject GetGameObject()
  {
    return gameObject;
  }
  
  public void Interact(GameObject interacter){ }
}