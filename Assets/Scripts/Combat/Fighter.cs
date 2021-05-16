using UnityEngine;
using RPG.Core;
using RPG.Stats;
using System.Collections;
using GameDevTV.Utils;
using RPG.Movement;

namespace RPG.Combat
{
  [RequireComponent(typeof(ActionScheduler), typeof(Mover))]
  public class Fighter : MonoBehaviour, IAction, IStatModifier
  {
    [SerializeField] Transform rightWeaponHolder;
    [SerializeField] Transform leftWeaponHolder;
    [SerializeField] Weapon defaultWeapon;

    Mover mover;
    Health target;
    ActionScheduler scheduler;
    Animator animator;
    LayerMask collisionLayer;
    LazyValue<Weapon> currentWeapon;
    [HideInInspector] public bool currentlyAttacking = false;

    public bool HasTarget => target != null;

    void Awake()
    {
      scheduler = GetComponent<ActionScheduler>();
      animator = GetComponent<Animator>();
      mover = GetComponent<Mover>();

      currentWeapon = new LazyValue<Weapon>(GetInitializeWeapon);
    }

    private Weapon GetInitializeWeapon()
    {
      SpawnWeapon(defaultWeapon);
      return defaultWeapon;
    }

    void Start()
    {
      currentWeapon.ForceInit();
    }

    void Update()
    {
      if (target == null) return;

      if (target.IsDead) Cancel();
      if (target && !currentlyAttacking) Attack();
    }

    public void SetCombatTarget(GameObject combatTarget, LayerMask layer)
    {
      collisionLayer = layer;
      scheduler.StartAction(this);
      target = combatTarget.GetComponent<Health>();
    }

    public virtual void Cancel()
    {
      StopAttacking();
      mover.Cancel();
      target = null;
    }

    void StopAttacking()
    {
      animator.SetTrigger("cancelAttack");
    }

    void Attack()
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

      /*damage is dealt by the animation Hit() event*/
      /*projectile for Ranged Weapons is launched within the Shoot() event */
    }


    IEnumerator StartAttacking()
    {
      currentlyAttacking = true;
      animator.ResetTrigger("cancelAttack");
      animator.SetTrigger("attack");
      AdjustAttackDirection();
      yield return new WaitForSeconds(1 / currentWeapon.value.AttackSpeed);
      currentlyAttacking = false;
    }

    bool TargetInRange()
    {
      return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.AttackRange;
    }

    void AdjustAttackDirection()
    {
      transform.LookAt(target.transform, Vector3.up);
    }

    GameObject weaponReference = null;
    public void EquipWeapon(Weapon weapon)
    {
      if (weaponReference) Destroy(weaponReference);
      SpawnWeapon(weapon);
      currentWeapon.value = weapon;
    }

    private void SpawnWeapon(Weapon weapon)
    {
      Animator animator = GetComponent<Animator>();
      weaponReference = weapon.Equip(rightWeaponHolder, leftWeaponHolder, animator);
    }

    //animation event (called from animator)
    void Hit()
    {
      if (target == null) return;

      float damage = GetComponent<CharacterStats>().GetStat(Stat.DAMAGE);
      if (currentWeapon.value is RangedWeapon)
      {
        RangedWeapon weapon = (RangedWeapon)currentWeapon.value;
        weapon.LaunchProjectile(rightWeaponHolder, leftWeaponHolder, target, gameObject, collisionLayer, damage);
      }
      else
      {
        if (!TargetInRange()) return;
        target.GetComponent<Health>().ApplyDamage(gameObject, damage);
      }
    }

    //animation event (called from animator)
    void Shoot()
    {
      Hit();
    }

    public IEnumerable GetAdditiveModifiers(Stat stat)
    {
      if (stat == Stat.DAMAGE)
      {
        yield return currentWeapon.value.Damage;
      }
    }

    public IEnumerable GetMultiplicativeModifiers(Stat stat)
    {
      if (stat == Stat.DAMAGE)
      {
        yield return currentWeapon.value.DamageMultiplier;
      }
    }
  }
}