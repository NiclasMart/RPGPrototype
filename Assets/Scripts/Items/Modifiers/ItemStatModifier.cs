using UnityEngine;
using UnityEngine.Events;
using UltEvents;

namespace RPG.Items
{
  public enum Rank
  {
    Normal,
    Rare,
    Epic,
    Legendary
  }

  [CreateAssetMenu(fileName = "ItemStatModifier", menuName = "Items/Create New ItemStatModifier", order = 0)]
  public class ItemStatModifier : ScriptableObject
  {
    public Rank rank;
    public string displayText;
    public int min, max;
    public UltEvent effect;

    public int GetRandomValue()
    {
      return Random.Range(min, max + 1);
    }
  }
}