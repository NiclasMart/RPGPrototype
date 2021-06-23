using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
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

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponentInChildren<Animator>();
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

      Vector3 lookPoint = RotateCharacter();

      //prepare cast and start animation
      scheduler.StartAction(castedAbility);
      castedAbility.PrepareCast(lookPoint, gameObject, castPosition, collisionLayer, animator);
      cooldownTable[castedAbility] = Time.time;
      animator.SetTrigger("cast");
      animator.SetTrigger("cast" + (index + 1));

      /* ability cast is triggert by animation event CastAction() */
    }

    private Vector3 RotateCharacter()
    {
      Vector3 lookPoint = GetComponent<PlayerCursor>().Position;
      transform.LookAt(lookPoint, Vector3.up);
      transform.Rotate(Vector3.up * castedAbility.animationRotationOffset);
      return lookPoint;
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
