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
    MeshRenderer graphicComponent;
    List<Health> targets = new List<Health>();
    public List<Health> TargetsInArea => targets;

    public bool locked = false;

    private void Awake()
    {
      graphicComponent = GetComponentInChildren<MeshRenderer>();
      graphicComponent.enabled = false;
    }

    private void Update()
    {
      if (Input.GetKey(KeyCode.Space)) graphicComponent.enabled = true;
      else graphicComponent.enabled = false;
    }

    private void FixedUpdate()
    {
      if (!locked) AdjustDirection();
    }

    public void AdjustDirection()
    {
      Vector3 lookPoint = PlayerInfo.GetPlayerCursor().Position;
      lookPoint.y = transform.position.y;
      transform.LookAt(lookPoint);
    }

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
