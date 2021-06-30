using System.Collections;
using RPG.Core;
using RPG.Items;
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
    }

    protected override void Initialize()
    {
      Ability baseAbility = Instantiate(ability, transform);
      CalculateInitialStats(baseAbility);
    }

    private void CalculateInitialStats(Ability ability)
    {
      ModifyTable statTable = new ModifyTable();
      ability.GetStats(statTable);
      GetComponent<CharacterStats>().RecalculateStats(statTable);
    }

    protected override IEnumerator StartAttacking()
    {
      isAttacking = true;
      animator.speed = animationSpeed;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      ability.PrepareCast(target.transform.position - transform.position, gameObject, castPosition, collisionLayer);
      yield return new WaitForSeconds(ability.animationClip.length * (1 / animationSpeed));
      animator.speed = 1f;
      isAttacking = false;
      scheduler.CancelCurrentAction();
    }

    void CastAction()
    {
      ability.CastAction();
    }
  }
}