using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
  public class AbilityManager : MonoBehaviour
  {
    [SerializeField] List<Ability> ablilities = new List<Ability>();
    [SerializeField] List<KeyCode> keyMap = new List<KeyCode>();
    [SerializeField] bool useKeyMap = false;
    public List<KeyCode> KeySet => keyMap;

    ActionScheduler scheduler;

    private void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
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
      scheduler.CancelCurrentAction();
      Vector3 lookDirection = GetComponent<PlayerCursor>().Position;
      transform.LookAt(lookDirection, Vector3.up);
      print("Cast: " + ablilities[index].name);
    }
  }
}
