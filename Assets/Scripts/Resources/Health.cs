using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Stats;
using RPG.Display;

namespace RPG.Resources
{
  public class Health : MonoBehaviour, IDisplayable
  {
    [SerializeField] HUDManager hudManager;
    Animator animator;

    float maxHealth;
    float currentHealth;
    bool isDead = false;

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Start()
    {
      animator = GetComponent<Animator>();
      currentHealth = maxHealth = GetComponent<BaseStats>().GetHealth();
      if (hudManager != null) hudManager.SetUpPlayerHealthBar(this);
    }

    public void ApplyDamage(GameObject instigator, float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      if (currentHealth == 0 && !isDead)
      {
        HandleDeath();
        EmitExperience(instigator);
      }
    }

    private void EmitExperience(GameObject instigator)
    {
      Experience playerExperience = instigator.GetComponent<Experience>();
      if (playerExperience)
      {
        BaseStats stats = GetComponent<BaseStats>();
        playerExperience.AddExperience(stats.GetExperiencePoints(), stats.Level);
      }
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

    public float GetCurrentValue()
    {
      return currentHealth;
    }

    public float GetMaxValue()
    {
      return maxHealth;
    }
  }
}
