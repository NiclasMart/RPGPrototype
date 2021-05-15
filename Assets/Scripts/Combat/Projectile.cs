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
    [SerializeField] bool homing = true;
    [SerializeField] Renderer graphicComponent;
    float maxTravelDistance;
    float damage;
    GameObject source;
    Health target;
    Vector3 startLocation;
    ProjectileCast castOrigin;

    bool active = false;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, GameObject source, float damage, float maxTravelDistance)
    {
      Initialize(source, damage, maxTravelDistance);

      this.target = target;
      transform.LookAt(AimLocation);
    }
    public void Initialize(Vector3 direction, ProjectileCast cast, Vector3 spawnPosition, float damage, float maxTravelDistance)
    {

      transform.position = spawnPosition;
      transform.forward = direction;
      castOrigin = cast;
      Initialize(cast.gameObject, damage, maxTravelDistance);

    }

    void Initialize(GameObject source, float damage, float maxTravelDistance)
    {

      this.source = source;
      this.damage = damage;
      this.maxTravelDistance = maxTravelDistance;
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
      graphicComponent.enabled = false;
      yield return new WaitForSeconds(GetComponentInChildren<TrailRenderer>().time);
      gameObject.SetActive(false);
      castOrigin.RepoolProjectile(this);
    }

    void Enable()
    {
      active = true;
      GetComponent<Collider>().enabled = true;
      graphicComponent.enabled = true;
      gameObject.SetActive(true);
      transform.GetComponentInChildren<TrailRenderer>().Clear();
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
