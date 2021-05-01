using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] float speed = 1f;
    [SerializeField] float destroyDelay = 0.1f;
    [SerializeField] bool homing = true;
    float maxTravelDistance;

    Health target;
    float damage;
    Vector3 startLocation;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, float damage, float maxTravelDistance)
    {
      this.target = target;
      this.damage = damage;
      this.maxTravelDistance = maxTravelDistance;
      startLocation = transform.position;

      transform.LookAt(AimLocation);
    }

    private void FixedUpdate()
    {
      if (target == null) Destroy(gameObject);
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
      target.ApplyDamage(damage);
      Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject == target.gameObject)
      {
        if (!target.IsDead) Impact();
      }
    }
  }
}
