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
        ablilities[i].OverrideAnimations(animator, "Cast" + (i + 1));
      }
    }

    public void CastAbility(KeyCode key, LayerMask collisionLayer)
    {
      int index = keyMap.IndexOf(key);
      if (index == -1) return;

      Ability castedAbility = ablilities[index];
      if (cooldownTable[castedAbility] + castedAbility.cooldown > Time.time) return;

      scheduler.CancelCurrentAction();
      Vector3 lookPoint = GetComponent<PlayerCursor>().Position;
      transform.LookAt(lookPoint, Vector3.up);
      cooldownTable[castedAbility] = Time.time;

      castedAbility.Cast(lookPoint - transform.position, gameObject, castPosition, collisionLayer);
      animator.SetTrigger("cast" + (index + 1));
    }
  }
}
