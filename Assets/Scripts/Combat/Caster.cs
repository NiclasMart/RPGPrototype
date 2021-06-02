using System.Collections;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class Caster : Attacker, IAction
  {
    [SerializeField] Ability ability;
    [SerializeField] Transform castPosition;

    private void Start()
    {
      AnimationHandler.OverrideAnimations(GetComponent<Animator>(), ability.animationClip, "Attack");
      Instantiate(ability.gameObject, transform);
    }

    public override void Attack()
    {
      if (TargetInRange())
      {
        StartCoroutine(StartAttacking());
        mover.Cancel();
      }
      else
      {
        StopAttacking();
        mover.MoveTo(target.transform.position);
      }
    }

    bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < ability.range;
    }

    IEnumerator StartAttacking()
    {
      currentlyAttacking = true;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      AdjustAttackDirection(ability.animationRotationOffset);
      ability.PrepareCast(target.transform.position - transform.position, gameObject, castPosition, collisionLayer, animator);
      yield return new WaitForSeconds(ability.cooldown);
      currentlyAttacking = false;
    }

    void CastAction()
    {
      ability.CastAction();
    }
  }
}