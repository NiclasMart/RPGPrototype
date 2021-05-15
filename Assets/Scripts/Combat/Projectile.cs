using UnityEngine;
using RPG.Stats;

namespace RPG.Combat
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] float speed = 1f;
    [SerializeField] float destroyDelay = 0.1f;
    [SerializeField] bool homing = true;
    float maxTravelDistance;

    GameObject source;
    Health target;
    Vector3 direction;
    float damage;
    Vector3 startLocation;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, GameObject source, float damage, float maxTravelDistance)
    {
      Initialize(source, damage, maxTravelDistance);

      this.target = target;
      transform.LookAt(AimLocation);
    }
    public void Initialize(Vector3 direction, GameObject source, float damage, float maxTravelDistance)
    {
      Initialize(source, damage, maxTravelDistance);

      this.direction = direction;
      transform.forward = direction;
    }

    void Initialize(GameObject source, float damage, float maxTravelDistance)
    {
      this.source = source;
      this.damage = damage;
      this.maxTravelDistance = maxTravelDistance;
      startLocation = transform.position;
    }

    private void FixedUpdate()
    {
      CheckTravelDistance();
      Move();
    }

    private void CheckTravelDistance()
    {
      float traveldDistance = Vector3.Distance(transform.position, startLocation);
      if (traveldDistance > 2 * maxTravelDistance) Destroy(gameObject);
    }

    private void Move()
    {
      if (homing) transform.LookAt(AimLocation);
      transform.Translate(Vector3.forward * speed);
    }

    private void Impact()
    {
      target.ApplyDamage(source, damage);
      Destroy(gameObject, destroyDelay);
    }

    private void Impact(Health target)
    {
      target.ApplyDamage(source, damage);
      Destroy(gameObject, destroyDelay);
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
