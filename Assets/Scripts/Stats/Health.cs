using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Display;

namespace RPG.Stats
{
  public class Health : MonoBehaviour, IDisplayable
  {
    [SerializeField] float lvlUpHeal = 0.3f;
    Animator animator;

    float maxHealth;
    float currentHealth;
    bool isDead = false;

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public ValueChangeEvent valueChange;

    private void Start()
    {
      animator = GetComponent<Animator>();
      currentHealth = maxHealth = GetComponent<CharacterStats>().GetStat(Stat.HEALTH);
      valueChange.Invoke(this);
    }

    public void ApplyDamage(GameObject instigator, float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      CheckForDeath(instigator);
      valueChange.Invoke(this);
    }

    private void CheckForDeath(GameObject instigator)
    {
      if (currentHealth == 0 && !isDead)
      {
        HandleDeath();
        EmitExperience(instigator);
      }
    }

    public void LevelUpHealth(CharacterStats stats)
    {
      maxHealth = stats.GetStat(Stat.HEALTH);
      HealPercentage(lvlUpHeal);
    }

    public void HealAbsolut(int value)
    {
      value = Mathf.Abs(value);
      currentHealth = Mathf.Min(maxHealth, currentHealth + value);
      valueChange.Invoke(this);
    }

    public void HealPercentage(float percent)
    {
      percent = Mathf.Abs(percent);
      currentHealth = Mathf.Min(maxHealth, currentHealth += currentHealth * percent);
      valueChange.Invoke(this);
    }

    private void EmitExperience(GameObject instigator)
    {
      Experience playerExperience = instigator.GetComponent<Experience>();
      if (playerExperience)
      {
        CharacterStats stats = GetComponent<CharacterStats>();
        playerExperience.GainExperience((int)stats.GetStat(Stat.EXPERIENCE_REWARD), stats.Level);
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
