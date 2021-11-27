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
      AbilityGem gem = (item as AbilityGem);
      if (gem == null) return;

      PlayerInfo.GetPlayer().GetComponent<AbilityManager>().SetNewAbility(gem, abilityCooldownDisplay);
    }

    protected override void UnequipCurrentItem()
    {
      connectedInventory.AddItem(item);
      PlayerInfo.GetPlayer().GetComponent<AbilityManager>().SetNewAbility(null, abilityCooldownDisplay);
    }
  }
}