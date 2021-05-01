using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
  public class BaseStats : MonoBehaviour
  {
    [SerializeField, Range(1, 99)] int level = 1;
    [SerializeField] CharakterClass charakterClass;
    [SerializeField] Progression progressionSet;

    public float GetHealth()
    {
      return progressionSet.GetHealth(charakterClass, level);
    }
  }
}
