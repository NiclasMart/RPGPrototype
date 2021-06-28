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
    [SerializeField] List<KeyCode> keyMap = new List<KeyCode>();
    [SerializeField] bool useKeyMap = false;
    public List<KeyCode> KeySet => keyMap;

    Dictionary<Ability, float> cooldownTable = new Dictionary<Ability, float>();
    ActionScheduler scheduler;
    Animator animator;
    Mover mover;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponentInChildren<Animator>();
      mover = GetComponent<Mover>();
      InstanciateAbilities();
      FillCooldownTable();
    }

    private void Start()
    {
      if (!useKeyMap) return;

      if (ablilities.Count != keyMap.Count)
      {
        Debug.LogError(transform.name + ": Abilitys and Key Map are inconsistent.");
      }

      for (int i = 0; i < ablilities.Count; i++)
      {
        AnimationHandler.OverrideAnimations(animator, ablilities[i].animationClip, "Cast" + (i + 1));
      }
    }

    Ability castedAbility;
    public void CastAbility(KeyCode key, LayerMask collisionLayer)
    {
      int index = keyMap.IndexOf(key);
      if (index == -1) return;

      castedAbility = ablilities[index];
      if (cooldownTable[castedAbility] + castedAbility.cooldown > Time.time) return;

      Vector3 lookPoint = GetComponent<PlayerCursor>().Position;

      //prepare cast and check if its valid 
      castedAbility.PrepareCast(lookPoint, gameObject, castPosition, collisionLayer, animator);
      if (!castedAbility.CastIsValid()) return;

      scheduler.StartAction(castedAbility);
      if (castedAbility.castImmediately) castedAbility.CastAction();
      RotateCharacter(lookPoint);

      cooldownTable[castedAbility] = Time.time;
      animator.SetTrigger("cast");
      animator.SetTrigger("cast" + (index + 1));

      /* ability cast is triggert by animation event CastAction() */
    }

    private void RotateCharacter(Vector3 lookPoint)
    {
      mover.AdjustDirection(lookPoint);
      transform.Rotate(Vector3.up * castedAbility.animationRotationOffset);
    }

    private void InstanciateAbilities()
    {
      List<Ability> abilityInstances = new List<Ability>();
      foreach (var ability in ablilities)
      {
        abilityInstances.Add(Instantiate(ability, transform));
      }
      ablilities = abilityInstances;
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
