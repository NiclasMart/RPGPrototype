using UnityEngine;

namespace RPG.Core
{
  public interface IInteraction
  {
    void Interact(GameObject interacter);
    GameObject GetGameObject();
  }
}