using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Interaction;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

public class LootChest : Interactable
{
  [SerializeField] Vector2 healAmount;

  public override void Interact(GameObject interacter)
  {
    LootGenerator.instance.DropLoot(1f, 0.75f, 3, transform.position, interacter.GetComponent<SoulEnergy>().GetSoulEnergyLevel());
    float healAmountPercent = Random.Range(healAmount.x, healAmount.y);
    interacter.GetComponent<Health>().HealPercentageMax(healAmountPercent);

    gameObject.SetActive(false);

  }
}
