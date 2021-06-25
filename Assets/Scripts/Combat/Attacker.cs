using System.Collections;
using RPG.Core;
using RPG.Movement;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler), typeof(Mover))]
  public class Attacker : MonoBehaviour, IAction
  {
    [SerializeField] float attackSpeedMuliplier = 0.4f;
    
    protected Health target;
    protected ActionScheduler scheduler;
    protected Animator animator;
    protected CharacterStats stats;
    protected LayerMask collisionLayer;

    [HideInInspector] public bool isAttacking = false;
    float lastAttackTime = Mathf.NegativeInfinity;

    protected virtual void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
      stats = GetComponent<CharacterStats>();
      Initialize();
    }

    protected virtual void Initialize() { }

    public virtual void Attack(Health combatTarget, LayerMask layer)
    {
      scheduler.StartAction(this);

      float attackSpeed = stats.GetStat(Stat.AttackSpeed) * attackSpeedMuliplier;
      if (lastAttackTime + (1 / attackSpeed) > Time.time) return;
      lastAttackTime = Time.time;

      collisionLayer = layer;
      target = combatTarget;
      StartCoroutine(StartAttacking());
    }

    protected virtual IEnumerator StartAttacking()
    {
      yield return null;
    }


    protected void StopAttacking()
    {
      animator.SetTrigger("cancelAttack");
    }


    public virtual void Cancel()
    {
      StopAttacking();
      target = null;
    }
  }
}