using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityManager : MonoBehaviour
  {
    [SerializeField] int collisionLayer;
    [SerializeField] Transform castPosition;
    [SerializeField] List<Ability> ablilities = new List<Ability>();
    [SerializeField] List<KeyCode> keyMap = new List<KeyCode>();
    [SerializeField] bool useKeyMap = false;
    public List<KeyCode> KeySet => keyMap;

    Dictionary<Ability, float> cooldownTable = new Dictionary<Ability, float>();
    ActionScheduler scheduler;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
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
      if (useKeyMap)
      {
        if (ablilities.Count != keyMap.Count)
        {
          Debug.LogError(transform.name + ": Abilitys and Key Map are inconsistent.");
        }
      }
    }

    public void CastAbility(KeyCode key)
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
    }
  }
}
