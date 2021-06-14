using System;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using UnityEngine;

namespace RPG.Interaction
{
  public class Inventory : MonoBehaviour
  {
    [HideInInspector] public ItemSlot selectedSlot;

    protected Color normalCol, rareCol, epicCol, legendaryCol;


    private void Awake()
    {
      InitializeColorParameters();
    }

    public void InitializeColorParameters()
    {
      GlobalParameters param = PlayerInfo.GetGlobalParameters();
      normalCol = param.normal;
      rareCol = param.rare;
      epicCol = param.epic;
      legendaryCol = param.legendary;
    }

    public virtual void SelectSlot(ItemSlot slot) { }

    public Color GetRarityColor(Rank rarity)
    {
      switch (rarity)
      {
        case Rank.Normal: return normalCol;
        case Rank.Rare: return rareCol;
        case Rank.Epic: return epicCol;
        case Rank.Legendary: return legendaryCol;
      }
      return normalCol;
    }
  }
}