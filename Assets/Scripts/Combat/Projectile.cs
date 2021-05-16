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
    [SerializeField] bool homing = true;
    [SerializeField] GameObject graphicComponent;
    float damage;
    GameObject source;
    Health target;
    Vector3 startLocation;
    ProjectileCast castOrigin;

    bool active = false;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, GameObject source, float damage, LayerMask collisionLayer)
    {
      Initialize(source, damage, collisionLayer);

      this.target = target;
      transform.LookAt(AimLocation);
    }
    public void Initialize(Vector3 direction, ProjectileCast cast, Vector3 spawnPosition, float damage, LayerMask collisionLayer)
    {

      transform.position = spawnPosition;
      transform.forward = direction;
      castOrigin = cast;
      Initialize(cast.gameObject, damage, collisionLayer);

    }

    void Initialize(GameObject source, float damage, LayerMask collisionLayer)
    {
      gameObject.layer = collisionLayer;
      this.source = source;
      this.damage = damage;
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
      target.ApplyDamage(source, damage);
      HandleDestruction();
    }

    private void Impact(Health target)
    {
      target.ApplyDamage(source, damage);
      HandleDestruction();

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


    private void OnTriggerEnter(Collider other)
    {
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
        if (hitTarget) Impact(hitTarget);
      }
    }
  }
}
