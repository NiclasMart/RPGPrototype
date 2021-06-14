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

    [Tooltip("Determines with which probability a second or third modifier is added to the item")]
    public float modifierProbability = 0.35f;

    [Tooltip("Determines the improvement of rare or better items")]
    public float rareValueImprovement = 0.3f;

    [Header("Rarity colors")]
    public Color normal, rare, epic, legendary;

  }
}
