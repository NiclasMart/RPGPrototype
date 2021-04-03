using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler))]
  public class Fighter : MonoBehaviour, IAction
  {
    [SerializeField] protected float attackRange = 1f;

    protected Transform target;
    protected ActionScheduler scheduler;

    public virtual void Start()
    {
      scheduler = GetComponent<ActionScheduler>();
    }

    private void Update()
    {
      if (target) Attack();
    }

    public void SetCombatTarget(Attackable combatTarget)
    {
      scheduler.StartAction(this);
      target = combatTarget.transform;
    }

    public virtual void Attack()
    {
      print("I will take all of your LOOT! YOU dumbass " + target.name);
    }

    public void Cancel()
    {
      target = null;
    }
  }
}