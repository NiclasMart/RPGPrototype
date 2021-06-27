using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Display
{
  public class StatsDisplay : MonoBehaviour
  {
    Dictionary<Stat, StatSlot> slots = new Dictionary<Stat, StatSlot>();
    private void Awake()
    {
      BuildSlotDictionary();
    }

    public void DisplayStat(Stat stat, float value)
    {
      if (!slots.ContainsKey(stat)) return;
      slots[stat].SetStatDisplay(value);
    }

    private void BuildSlotDictionary()
    {
      
      foreach (var slot in GetComponentsInChildren<StatSlot>(true))
      {
        if (slots.ContainsKey(slot.stat)) continue;
        slots.Add(slot.stat, slot);
      }
    }
  }
}