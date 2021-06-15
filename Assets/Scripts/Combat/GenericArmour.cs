using RPG.Items;
using UnityEngine;

namespace RPG.Combat
{
  [CreateAssetMenu(fileName = "GenericArmour", menuName = "Items/Armour/Create New GenericArmour", order = 0)]
  public class GenericArmour : GenericItem
  {
    [Header("Armour Parameters")]
    [SerializeField] Vector2 armour, magicResistance;

    public override Item GenerateItem()
    {
      return new Armour(this);
    }

    public float GetArmour() { return Random.Range(armour.x, armour.y); }
    public float GetMagicResi() { return Random.Range(magicResistance.x, magicResistance.y); }
  }
}