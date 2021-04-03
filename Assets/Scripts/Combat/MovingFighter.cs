using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(Mover))]
  public class MovingFighter : Fighter
  {
    Mover mover;

    public override void Start()
    {
      base.Start();
      mover = GetComponent<Mover>();
    }

    public override void Attack()
    {
      bool inAttackRange = Vector3.Distance(transform.position, target.transform.position) <= attackRange;
      if (!inAttackRange)
      {
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;
        Vector3 attackPosition = target.transform.position - targetDirection * attackRange;
        mover.MoveTo(attackPosition);
      }
      else
      {
        print("do damage");
      }


    }
  }
}