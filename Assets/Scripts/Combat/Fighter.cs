using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float timeBetweenAttacks = 1f;

    protected Transform target;
    protected ActionScheduler scheduler;
    protected Animator animator;

    protected virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
      if (target) Attack();
    }

    public void SetCombatTarget(GameObject combatTarget)
    {
      scheduler.StartAction(this);
      target = combatTarget.transform;
      animator.ResetTrigger("cancelAttack");
    }

    public void Cancel()
    {
      animator.SetTrigger("cancelAttack");
      target = null;
    }

    protected virtual void Attack()
    {
      if (TargetInRange())
      {
        PlayAttackAnimation();
        AdjustAttackDirection();
        /*damage is dealt by the animation Hit() event*/
      }
    }

    protected bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < attackRange;
    }

    protected void AdjustAttackDirection()
    {
      transform.LookAt(target, Vector3.up);
    }

    float attackState = 0f;
    float lastAttackTime = -Mathf.Infinity;
    protected void PlayAttackAnimation()
    {
      if (lastAttackTime + timeBetweenAttacks <= Time.time)
      {
        //switch between thw different attack animations
        animator.SetFloat("attackState", attackState);
        attackState = (++attackState % 2);
        //set up animation
        animator.SetTrigger("attack");
        lastAttackTime = Time.time;
      }
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target)
      {
        bool targetDies = target.GetComponent<Health>().ApplyDamage(damage);
        if (targetDies) Cancel();
        print("do damage");
      }
    }
  }
}