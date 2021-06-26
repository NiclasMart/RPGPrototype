using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class TargetDetector : MonoBehaviour
  {
    string avoidTag;
    MeshRenderer graphicComponent;
    List<Health> targets = new List<Health>();
    public List<Health> TargetsInArea => targets;

    public bool locked = false;

    private void Awake()
    {
      graphicComponent = GetComponentInChildren<MeshRenderer>();
      graphicComponent.enabled = false;
    }

    public void Initialize(string tag)
    {
      avoidTag = tag;
    }

    public void Toggle(bool show)
    {
      graphicComponent.enabled = show;
    }

    public void AdjustDirection(Vector3 lookPoint)
    {
      if (locked) return;

      lookPoint.y = transform.position.y;
      transform.LookAt(lookPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(avoidTag)) return;

      Health target = other.GetComponent<Health>();
      if (target && !targets.Contains(target)) targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag(avoidTag)) return;

      Health target = other.GetComponent<Health>();
      if (target) targets.Remove(target);
    }
  }
}
