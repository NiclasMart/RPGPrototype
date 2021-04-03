using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(Mover))]
  public class MovingFighter : Fighter
  {
    Mover mover;

    private void Start()
    {
      mover = GetComponent<Mover>();
    }

    public override void Attack()
    {
      Vector3 targetDirection = (target.transform.position - transform.position).normalized;
      Vector3 attackPosition = target.transform.position - targetDirection * attackRange;
      mover.MoveTo(attackPosition);
    }
  }
}