using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityManager : MonoBehaviour, IAction
  {
    [SerializeField] Transform castPosition;
    [SerializeField] List<AbilityCooldownDisplay> slots = new List<AbilityCooldownDisplay>();
    [SerializeField] List<Ability> startAbilities = new List<Ability>();
    Dictionary<KeyCode, AbilityCooldownDisplay> abilitySlots = new Dictionary<KeyCode, AbilityCooldownDisplay>();
    //List<Ability> abilities = new List<Ability>();
    public Dictionary<KeyCode, AbilityCooldownDisplay>.KeyCollection KeySet => abilitySlots.Keys;

    ActionScheduler scheduler;
    Animator animator;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponentInChildren<Animator>();
      mover = GetComponent<Mover>();

      Initialize();
    }

    bool initialized = false;
    private void Initialize()
    {
      if (initialized) return;
      BuildSlotDictionary();
      InitializeStartAbilities();
      initialized = true;
    }

    private void Start()
    {
      for (int i = 0; i < startAbilities.Count; i++)
      {
        if (startAbilities[i] == null) continue;
        AnimationHandler.OverrideAnimations(animator, startAbilities[i].animationClip, "Cast" + (i + 1));
      }
    }

    Ability castedAbility;
    public void CastAbility(KeyCode key, LayerMask collisionLayer)
    {
      AbilityCooldownDisplay abilitySlot = abilitySlots[key];

      if (abilitySlot.ability == null) return;
      if (!abilitySlot.CooldownReady()) return;

      castedAbility = abilitySlot.ability;
      Vector3 lookPoint = PlayerInfo.GetPlayerCursor().Position;

      //prepare cast and check if its valid 
      castedAbility.PrepareCast(lookPoint, gameObject, castPosition, collisionLayer);
      if (!castedAbility.CastIsValid()) return;

      //cast and set cooldown
      if (!scheduler.StartAction(this, true)) return;
      abilitySlot.SetCooldown();
      RotateCharacter(lookPoint);
      if (castedAbility.castImmediately) castedAbility.CastAction();

      //handle animation
      animator.SetTrigger("cast");
      animator.SetTrigger("cast" + abilitySlot.index);

      /* ability cast is triggert by animation event CastAction() */
    }

    public void SetNewAbility(Ability newAbility, float damage, AbilityCooldownDisplay slot)
    {
      Ability abilityInstance = InstanciateAbility(newAbility);
      slot.SetAbility(abilityInstance);
      if (newAbility == null) return;
      abilityInstance.baseDamage = damage;
      AnimationHandler.OverrideAnimations(animator, newAbility.animationClip, "Cast" + slot.index);
    }

    public Ability GetRollAbility()
    {
      Initialize();
      return abilitySlots[slots[0].activationKey].ability;
    }

    private void InitializeStartAbilities()
    {
      for (int i = 0; i < startAbilities.Count; i++)
      {
        Ability abilityInstance = InstanciateAbility(startAbilities[i]);
        slots[i].SetAbility(abilityInstance);
      }
    }

    private void BuildSlotDictionary()
    {
      CharacterStats stats = GetComponent<CharacterStats>();
      for (int i = 0; i < slots.Count; i++)
      {
        abilitySlots.Add(slots[i].activationKey, slots[i]);
        slots[i].Initialize(stats);
      }
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

    //called by animation
    void CastAction()
    {
      castedAbility.CastAction();
    }

    //called by animation
    void FinishedCast()
    {
      scheduler.ReleaseLock();
    }

    public void Cancel()
    {
      //animator.SetTrigger("cancelAttack");
    }
  }
}
