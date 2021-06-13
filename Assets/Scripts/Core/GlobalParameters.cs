using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  [CreateAssetMenu(fileName = "GlobalParameters", menuName = "RPGPrototype/Data", order = 0)]
  public class GlobalParameters : MonoBehaviour
  {
    [Header("Drop Parameter")]
    public float normalDropRate = 0.74f;
    public float rareDropRate = 0.18f;
    public float epicDropRate = 0.06f;
    public float uniqueDropRate = 0.02f;
    public float modifierProbability = 0.35f;

  }
}
