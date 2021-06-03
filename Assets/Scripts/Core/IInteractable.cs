using UnityEngine;

namespace RPG.Core
{
  public interface IInteractable
  {
    void Interact(GameObject interacter);
    GameObject GetGameObject();
  }
}