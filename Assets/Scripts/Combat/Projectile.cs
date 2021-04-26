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
    [SerializeField] bool homing = true;

    Health target;
    float damage;

    Vector3 AimLocation => target.GetComponent<Collider>().bounds.center;

    public void Initialize(Health target, float damage)
    {
      this.target = target;
      this.damage = damage;

      transform.LookAt(AimLocation);
    }

    private void FixedUpdate()
    {
      if (target == null) Destroy(gameObject);

      Move();
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
