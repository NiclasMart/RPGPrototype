using System;
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
      public string name;
      public float value;
      public string display;
      public Rank rarity;
      public UltEvent<ModifyTable, float> effect;

      public Modifier(ItemStatModifier baseModifier)
      {
        name = baseModifier.name;
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

    [Serializable]
    public class SerializableModifier
    {
      public string name;
      public float value;

      public SerializableModifier(Modifier mod)
      {
        this.name = mod.name;
        this.value = mod.value;
      }

      public Modifier ConvertToModifier()
      {
        ItemStatModifier genericMod = Resources.Load("Items/_Modifiers/" + name) as ItemStatModifier;
        if (genericMod == null) return null;
        Modifier mod = new Modifier(genericMod);
        mod.value = value;
        return mod;
      }
    }

    [Serializable]
    public class MSaveData : SaveData
    {
      public List<ModifiableItem.SerializableModifier> modifiers;

      public MSaveData(Item item) : base(item)
      {
        modifiers = (item as ModifiableItem).GetSerializableModifiers();
      }

      public override Item CreateItemFromData()
      {
        ModifiableItem mItem = base.CreateItemFromData() as ModifiableItem;
        mItem.DeserializeModifiers(modifiers);
        return mItem;
      }
    }

    public List<Modifier> modifiers = new List<Modifier>();

    public ModifiableItem(GenericItem baseItem) : base(baseItem) { }

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

    public void AddModifier(ItemStatModifier newModifier)
    {
      modifiers.Add(new Modifier(newModifier));
    }

    public List<SerializableModifier> GetSerializableModifiers()
    {
      List<SerializableModifier> sMods = new List<SerializableModifier>();
      foreach (var mod in modifiers)
      {
        sMods.Add(new SerializableModifier(mod));
      }
      return sMods;
    }

    public void DeserializeModifiers(List<SerializableModifier> sMods)
    {
      foreach (var smod in sMods)
      {
        modifiers.Add(smod.ConvertToModifier());
      }
    }

    public override object GetSaveData()
    {
      return new MSaveData(this);
    }

    public virtual void GetStats(ModifyTable stats) { }
  }
}