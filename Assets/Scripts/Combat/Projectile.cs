using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
  public class Projectile : MonoBehaviour
  {
    [SerializeField] float speed = 1f;

    Transform target;
    float damage;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Transform target, float damage)
    {
      this.target = target;
      this.damage = damage;
    }

    private void FixedUpdate()
    {
      if (target == null) return;

      transform.LookAt(AimLocation);
      transform.Translate(Vector3.forward * speed);
    }

    private void ApplyDamage()
    {
      Health health = target.GetComponent<Health>();
      if (health) health.ApplyDamage(damage);
      Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject == target.gameObject)
      {
        ApplyDamage();
      }
    }
  }
}
