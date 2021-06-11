using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Items
{
  [CreateAssetMenu(fileName = "Item", menuName = "RPGPrototype/Item", order = 0)]
  public class GenericItem : ScriptableObject
  {
    public string baseResourcePath = "ItemSprites/";
    public Sprite icon;
    public GameObject itemObject;
    public float weight;
    public ItemType itemType;
    public bool modifiable = false;
    public List<ItemStatModifier> modifiers = new List<ItemStatModifier>();

    public virtual Item GenerateItem()
    {
      return new Item(this);
    }
  }
}