using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [CreateAssetMenu(fileName = "Item", menuName = "Items/Create New GenericItem", order = 0)]
  public class GenericItem : ScriptableObject
  {
    public string name;
    public string baseResourcePath = "ItemSprites/";
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public ItemType itemType;
    public bool modifiable = false;
    public List<ItemStatModifier> normalModifiers = new List<ItemStatModifier>();
    public List<ItemStatModifier> epicModifiers = new List<ItemStatModifier>();
    public List<ItemStatModifier> legendaryModifiers = new List<ItemStatModifier>();

    public virtual Item GenerateItem()
    {
      return new Item(this);
    }
  }
}