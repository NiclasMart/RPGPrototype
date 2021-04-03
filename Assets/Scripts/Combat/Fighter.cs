using UnityEngine;

namespace RPG.Combat
{

  public class Fighter : MonoBehaviour
  {
    [SerializeField] protected float attackRange = 1f;

    protected Transform target;

    private void Update()
    {
      if (target) Attack();
    }

    public void SetCombatTarget(Attackable combatTarget)
    {
      target = combatTarget.transform;
    }

    public virtual void Attack()
    {
      print("I will take all of your LOOT! YOU dumbass " + target.name);
    }

    public void ResetCombatTarget()
    {
      target = null;
    }
  }
}