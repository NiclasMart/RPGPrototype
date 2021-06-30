using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityManager : MonoBehaviour
  {
    [SerializeField] Transform castPosition;
    [SerializeField] List<Ability> ablilities = new List<Ability>();
    [SerializeField] List<AbilityCooldownDisplay> slots = new List<AbilityCooldownDisplay>();
    Dictionary<KeyCode, AbilityCooldownDisplay> abilitySlots = new Dictionary<KeyCode, AbilityCooldownDisplay>();
    public Dictionary<KeyCode, AbilityCooldownDisplay>.KeyCollection KeySet => abilitySlots.Keys;

    Dictionary<Ability, float> cooldownTable = new Dictionary<Ability, float>();
    ActionScheduler scheduler;
    Animator animator;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponentInChildren<Animator>();
      mover = GetComponent<Mover>();
      BuildSlotDictionary();
    }

    private void BuildSlotDictionary()
    {
      for (int i = 0; i < slots.Count; i++)
      {
        abilitySlots.Add(slots[i].activationKey, slots[i]);

        Ability abilityInstance = InstanciateAbility(ablilities[i]);

        slots[i].SetAbility(abilityInstance);
      }
    }

    private void Start()
    {
      for (int i = 0; i < ablilities.Count; i++)
      {
        if (ablilities[i] == null) continue;
        AnimationHandler.OverrideAnimations(animator, ablilities[i].animationClip, "Cast" + (i + 1));
      }
    }

    Ability castedAbility;
    public void CastAbility(KeyCode key, LayerMask collisionLayer)
    {

      AbilityCooldownDisplay abilitySlot = abilitySlots[key];

      if (!abilitySlot.CooldownReady()) return;

      castedAbility = abilitySlot.ability;
      Vector3 lookPoint = GetComponent<PlayerCursor>().Position;

      //prepare cast and check if its valid 
      castedAbility.PrepareCast(lookPoint, gameObject, castPosition, collisionLayer);
      if (!castedAbility.CastIsValid()) return;

      scheduler.StartAction(castedAbility);
      if (castedAbility.castImmediately) castedAbility.CastAction();
      abilitySlot.SetCooldown();
      RotateCharacter(lookPoint);

      cooldownTable[castedAbility] = Time.time;
      animator.SetTrigger("cast");
      int index = ablilities.FindIndex(x => x != null && x.name == castedAbility.name);
      animator.SetTrigger("cast" + (index + 1));

      /* ability cast is triggert by animation event CastAction() */
    }

    private void RotateCharacter(Vector3 lookPoint)
    {
      mover.AdjustDirection(lookPoint);
      transform.Rotate(Vector3.up * castedAbility.animationRotationOffset);
    }

    private Ability InstanciateAbility(Ability ability)
    {
      if (ability != null) return Instantiate(ability, transform);
      else return null;
    }

    private void FillCooldownTable()
    {
      foreach (Ability ability in ablilities)
      {
        cooldownTable.Add(ability, -ability.cooldown);
        if (ability.cooldown < ability.animationClip.length)
        {
          Debug.LogError("ERROR: Ability cooldown from " + name + " needs to be longer than animation clip length!");
        }
      }
    }

    void CastAction()
    {
      castedAbility.CastAction();
    }
  }
}
