using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Display;
using GameDevTV.Utils;
using System;

namespace RPG.Stats
{
  public class Health : MonoBehaviour, IDisplayable, IInteraction
  {
    [SerializeField] float lvlUpHeal = 0.3f;
    Animator animator;

    LazyValue<float> maxHealth;
    LazyValue<float> currentHealth;
    bool isDead = false;

    public bool IsDead => isDead;
    public float CurrentHealth => currentHealth.value;
    public float MaxHealth => maxHealth.value;

    public ValueChangeEvent valueChange;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
      maxHealth = new LazyValue<float>(GetInitializeHealth);
      currentHealth = new LazyValue<float>(GetInitializeHealth);
    }

    private float GetInitializeHealth()
    {
      return GetComponent<CharacterStats>().GetStat(Stat.HEALTH);
    }

    private void Start()
    {
      currentHealth.ForceInit();
      maxHealth.ForceInit();
      valueChange.Invoke(this);
    }

    public void ApplyDamage(GameObject instigator, float damage)
    {
      currentHealth.value = Mathf.Max(0, currentHealth.value - damage);
      CheckForDeath(instigator);
      valueChange.Invoke(this);
    }

    private void CheckForDeath(GameObject instigator)
    {
      if (currentHealth.value == 0 && !isDead)
      {
        HandleDeath();
        EmitExperience(instigator);
      }
    }

    public void LevelUpHealth(CharacterStats stats)
    {
      maxHealth.value = stats.GetStat(Stat.HEALTH);
      HealPercentage(lvlUpHeal);
    }

    public void HealAbsolut(int value)
    {
      value = Mathf.Abs(value);
      currentHealth.value = Mathf.Min(maxHealth.value, currentHealth.value + value);
      valueChange.Invoke(this);
    }

    public void HealPercentage(float percent)
    {
      percent = Mathf.Abs(percent);
      currentHealth.value = Mathf.Min(maxHealth.value, currentHealth.value += currentHealth.value * percent);
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
      DisableComponents();
      isDead = true;
    }

    private void DisableComponents()
    {
      if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
      foreach (Collider col in GetComponentsInChildren<Collider>())
      {
        col.enabled = false;
      }
    }

    public float GetCurrentValue()
    {
      return currentHealth.value;
    }

    public float GetMaxValue()
    {
      return maxHealth.value;
    }

    public void Interact(GameObject interacter){ }

    public GameObject GetGameObject()
    {
      return gameObject;
    }
  }
}
