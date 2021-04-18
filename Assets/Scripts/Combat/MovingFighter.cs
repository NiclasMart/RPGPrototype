using UnityEngine;
using RPG.Movement;
using System;

namespace RPG.Combat
{
  [RequireComponent(typeof(Mover))]
  public class MovingFighter : Fighter
  {
    Mover mover;

    protected override void Start()
    {
      base.Start();
      mover = GetComponent<Mover>();
    }

    protected override void Attack()
    {
      if (!TargetInRange())
      {
        mover.MoveTo(target.transform.position);
      }
      else
      {
        mover.Cancel();
        AdjustAttackDirection();
        PlayAttackAnimation();
      }
    }

    public override void Cancel()
    {
      base.Cancel();
      mover.Cancel();
    }
  }
}