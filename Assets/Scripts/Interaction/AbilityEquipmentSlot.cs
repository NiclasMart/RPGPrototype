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

    public override void Select()
    {
      base.Select();
      connectedInventory.onSecondClick += SetAbility;
    }

    public override void Deselect()
    {
      connectedInventory.onSecondClick -= SetAbility;
    }

    private void SetAbility(Item item)
    {
      Ability newAbility = (item as AbilityHolder)?.ability;
      PlayerInfo.GetPlayer().GetComponent<AbilityManager>().SetNewAbility(newAbility, abilityCooldownDisplay);
      SetIcon(item);
      this.item = item;
    }
  }
}