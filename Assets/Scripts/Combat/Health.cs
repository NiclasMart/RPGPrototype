using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float startHealth = 100f;
    Animator animator;
    float currentHealth;
    bool isDead = false;

    private void Start()
    {
      currentHealth = startHealth;
      animator = GetComponent<Animator>();
    }

    public bool ApplyDamage(float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      if (currentHealth == 0 && !isDead)
      {
        HandleDeath();
      }
      print("current Health: " + currentHealth);
      return isDead;
    }

    private void HandleDeath()
    {
      if (animator != null) animator.SetTrigger("die");
      DisableAllColliders();
      isDead = true;
    }

    private void DisableAllColliders()
    {
      if (TryGetComponent(out Collider collider)) collider.enabled = false;
      if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
    }
  }
}
