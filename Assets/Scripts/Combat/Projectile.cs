using UnityEngine;
using RPG.Stats;
using System;
using System.Collections;

namespace RPG.Combat
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] float speed = 1f;
    [SerializeField] float destroyDelay = 0.1f;
    [SerializeField] float maxTravelDistance = 10f;
    [SerializeField] float baseDamageModifier = 1f;
    [SerializeField] bool homing = true;
    [SerializeField] bool penetrating = false;
    [SerializeField] GameObject graphicComponent;
    DamageType damageType;
    float baseDamage;
    GameObject source;
    Health target;
    Vector3 startLocation;
    ProjectileCast castOrigin;

    bool active = false;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    //arrow projectile
    public void Initialize(Vector3 direction, GameObject source, float damage, LayerMask collisionLayer)
    {
      Initialize(source, damage, collisionLayer);
      transform.forward = direction;
      damageType = DamageType.physicalDamage;
    }

    //ability projectile
    public void Initialize(Vector3 direction, GameObject source, ProjectileCast cast, Vector3 spawnPosition, float damage, float maxTravelDistance, DamageType damageType, LayerMask collisionLayer)
    {

      transform.position = spawnPosition;
      startLocation = spawnPosition;
      transform.forward = direction;
      castOrigin = cast;
      this.maxTravelDistance = maxTravelDistance;
      this.damageType = damageType;
      Initialize(source, damage, collisionLayer);

    }

    void Initialize(GameObject source, float damage, LayerMask collisionLayer)
    {
      gameObject.layer = collisionLayer;
      this.source = source;
      this.baseDamage = damage;
      startLocation = transform.position;
      Enable();
    }

    private void FixedUpdate()
    {
      if (!active) return;
      CheckTravelDistance();
      Move();
    }

    private void CheckTravelDistance()
    {
      float traveldDistance = Vector3.Distance(transform.position, startLocation);
      if (traveldDistance > 2 * maxTravelDistance) HandleDestruction();
    }

    private void Move()
    {
      if (homing) transform.LookAt(AimLocation);
      transform.Translate(Vector3.forward * speed);
    }

    private void Impact()
    {
      DealDamage(target);
      if (penetrating) return;
      HandleDestruction();
      active = false;
    }

    private void Impact(Health hitTarget)
    {
      DealDamage(hitTarget);
      if (penetrating) return;
      HandleDestruction();
      active = false;
    }

    private void DealDamage(Health target)
    {
      bool isCrit = false;
      float finalDamage = (damageType == DamageType.physicalDamage)
        ? DamageCalculator.CalculatePhysicalDamage(source.GetComponent<CharacterStats>(), target.GetComponent<CharacterStats>(), baseDamage, baseDamageModifier, ref isCrit)
        : DamageCalculator.CalculateMagicDamage(source.GetComponent<CharacterStats>(), target.GetComponent<CharacterStats>(), baseDamage, baseDamageModifier);

      target.ApplyDamage(source, finalDamage);
    }

    void HandleDestruction()
    {
      if (castOrigin) StartCoroutine(Disable());
      else Destroy(gameObject, destroyDelay);
    }

    IEnumerator Disable()
    {
      active = false;
      GetComponent<Collider>().enabled = false;
      graphicComponent.SetActive(false);
      yield return new WaitForSeconds(0.4f);
      gameObject.SetActive(false);
      castOrigin.RepoolProjectile(this);
    }

    void Enable()
    {
      active = true;
      GetComponent<Collider>().enabled = true;
      graphicComponent.SetActive(true);
      gameObject.SetActive(true);
      TrailRenderer trail = transform.GetComponentInChildren<TrailRenderer>();
      if (trail) trail.Clear();
    }

    Collider lastTarget;
    private void OnTriggerEnter(Collider other)
    {
      if (!active) return;

      if (target)
      {
        if (other.gameObject == target.gameObject)
        {
          if (!target.IsDead) Impact();
        }
      }
      else
      {
        Health hitTarget = other.GetComponent<Health>();

        if (hitTarget)
        {
          if (other == lastTarget) return;
          lastTarget = other;
          Impact(hitTarget); Debug.Log("Hit " + other.name);
        }
      }
    }
  }
}
