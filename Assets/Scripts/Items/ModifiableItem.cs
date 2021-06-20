using System.Collections.Generic;
using RPG.Core;
using UltEvents;
using UnityEngine;

namespace RPG.Items
{
  public class ModifiableItem : Item
  {
    public class Modifier
    {
      public float value;
      public string display;
      public Rank rarity;
      public UltEvent<ModifyTable, float> effect;

      public Modifier(ItemStatModifier baseModifier)
      {
        value = baseModifier.GetRandomValue();
        display = baseModifier.displayText;
        rarity = baseModifier.rank;
        effect += baseModifier.effect.InvokeX<ModifyTable, float>;
      }

      public string GetDisplayText()
      {
        return display.Replace("*", value.ToString());
      }
    }

    public static Color GetRarityColor(Rank rarity)
    {
      switch (rarity)
      {
        case Rank.Rare: return PlayerInfo.GetGlobalParameters().rare;
        case Rank.Epic: return PlayerInfo.GetGlobalParameters().epic;
        case Rank.Legendary: return PlayerInfo.GetGlobalParameters().legendary;
        default: return PlayerInfo.GetGlobalParameters().normal;
      }
    }

    public List<Modifier> modifiers = new List<Modifier>();
    public ModifiableItem(GenericItem baseItem) : base(baseItem) { }

    public void AddModifier(ItemStatModifier newModifier)
    {
      modifiers.Add(new Modifier(newModifier));
    }

    public virtual void GetStats(ModifyTable stats) { }
  }
}