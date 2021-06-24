using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class TargetDetector : MonoBehaviour
  {
    List<Health> targets = new List<Health>();
    public List<Health> TargetsInArea => targets;

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Player") return;
      Health target = other.GetComponent<Health>();
      if (target && !targets.Contains(target)) targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Player") return;

      Health target = other.GetComponent<Health>();
      if (target) targets.Remove(target);
    }
  }
}
