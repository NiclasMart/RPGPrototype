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
      animator = GetComponent<Animator>();
      FillCooldownTable();
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

      //set direction of character
      Vector3 lookPoint = GetComponent<PlayerCursor>().Position;
      transform.LookAt(lookPoint, Vector3.up);
      transform.Rotate(Vector3.up * castedAbility.animationRotationOffset);

      //prepare cast and start animation
      scheduler.StartAction(castedAbility);
      castedAbility.PrepareCast(lookPoint - transform.position, gameObject, castPosition, collisionLayer, animator);
      cooldownTable[castedAbility] = Time.time;
      animator.SetTrigger("cast");
      animator.SetTrigger("cast" + (index + 1));

      /* ability cast is triggert by animation event CastAction() */
    }

    void CastAction()
    {
      castedAbility.CastAction();
    }
  }
}
