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
    [SerializeField] float destroyDelay = 0.1f;

    Health target;
    float damage;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, float damage)
    {
      this.target = target;
      this.damage = damage;
    }

    private void FixedUpdate()
    {
      if (target == null) Destroy(gameObject);

      transform.LookAt(AimLocation);
      transform.Translate(Vector3.forward * speed);
    }

    private void ApplyDamage()
    {
      target.ApplyDamage(damage);
      Destroy(gameObject, destroyDelay);
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
