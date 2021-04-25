using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
  public class Health : MonoBehaviour
  {
    [SerializeField] float startHealth = 100f;
    Animator animator;
    float currentHealth;
    bool isDead = false;
    public bool IsDead => isDead;

    private void Start()
    {
      currentHealth = startHealth;
      animator = GetComponent<Animator>();
    }

    public void ApplyDamage(float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      if (currentHealth == 0 && !isDead)
      {
        HandleDeath();
      }
      print("current Health: " + currentHealth);
    }

    private void HandleDeath()
    {
      if (animator != null) animator.SetTrigger("die");
      GetComponent<ActionScheduler>().CancelCurrentAction();
      DisableNavAgent();
      isDead = true;
    }

    private void DisableNavAgent()
    {
      if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
    }
  }
}
