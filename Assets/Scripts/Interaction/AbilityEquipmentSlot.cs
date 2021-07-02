using RPG.Combat;
using RPG.Core;
using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class AbilityEquipmentSlot : EquipmentSlot
  {
    [SerializeField] AbilityCooldownDisplay abilityCooldownDisplay;

    public override void EquipItem(Item item)
    {
      base.EquipItem(item);
      Ability newAbility = (item as AbilityHolder)?.ability;
      PlayerInfo.GetPlayer().GetComponent<AbilityManager>().SetNewAbility(newAbility, abilityCooldownDisplay);
    }
  }
}