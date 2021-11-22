using UnityEngine;
using UnityEngine.Events;
using UltEvents;

namespace RPG.Items
{
  [System.Serializable]
  public enum Rank
  {
    Normal,
    Rare,
    Epic,
    Legendary
  }

  [CreateAssetMenu(fileName = "ItemStatModifier", menuName = "Items/Modifiers/Create New ItemStatModifier", order = 0)]
  public class ItemStatModifier : ScriptableObject
  {
    public Rank rank;
    public string displayText;
    public int min, max;
    public UltEvent<ModifyTable, float> effect;
    public UltEvent<float> legendaryEffect;

    public int GetRandomValue()
    {
      return Random.Range(min, max + 1);
    }

    public virtual void ModifyStat(ModifyTable stats, float value) { }
  }
}