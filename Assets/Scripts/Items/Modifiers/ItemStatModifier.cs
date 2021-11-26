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
    Legendary,
    Gem
  }

  [CreateAssetMenu(fileName = "ItemStatModifier", menuName = "Items/Modifiers/Create New ItemStatModifier", order = 0)]
  public class ItemStatModifier : ScriptableObject
  {
    public Rank rank;
    public string displayText;
    public int min, max;
    public UltEvent<ModifyTable, float> effect;
    public UltEvent<float> legendaryInstallEffect;
    public UltEvent legendaryUninstallEffect;

    public int GetRandomValue(float quality)
    {
      float lowQuality, highQuality;
      int lokalMin, lokalMax;
      lowQuality = Mathf.Max(0, quality - 0.1f);
      highQuality = Mathf.Min(1, quality + 0.1f);
      lokalMin = Mathf.FloorToInt(Mathf.Lerp(min, max, lowQuality));
      lokalMax = Mathf.RoundToInt(Mathf.Lerp(min, max, highQuality));
      return Random.Range(lokalMin, lokalMax + 1);
    }

    public virtual void ModifyStat(ModifyTable stats, float value) { }
  }
}