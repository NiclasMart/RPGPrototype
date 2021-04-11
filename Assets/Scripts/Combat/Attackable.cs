using UnityEngine;

namespace RPG.Combat
{
  [RequireComponent(typeof(Health))]
  public class Attackable : MonoBehaviour
  {
    public bool CanBeAttacked()
    {
      return !GetComponent<Health>().IsDead;
    }
  }
}