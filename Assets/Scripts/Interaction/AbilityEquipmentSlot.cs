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
    AbilityManager abilityManager;

    private void Start()
    {
      PlayerInfo.GetPlayer().GetComponent<AbilityManager>();
    }

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
      Debug.Log("SetAbility");
    }
  }
}