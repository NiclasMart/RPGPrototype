using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Display;
using GameDevTV.Utils;
using RPG.Items;
using RPG.Combat;
using System;
using RPG.Saving;
using Display;

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
    public AlterValue onTakeDamage;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
      maxHealth = new LazyValue<float>(GetInitializeHealth);
      currentHealth = new LazyValue<float>(GetInitializeHealth);

      GetComponent<CharacterStats>().statsChange += UpdateMaxHealth;
    }

    private float GetInitializeHealth()
    {
      return GetComponent<CharacterStats>().GetStat(Stat.Health);
    }

    private void Start()
    {
      currentHealth.ForceInit();
      maxHealth.ForceInit();
      valueChange.Invoke(this);
    }

    public void ApplyDamage(GameObject instigator, float damage)
    {
      onTakeDamage?.Invoke(ref damage);
      currentHealth.value = Mathf.Max(0, currentHealth.value - damage);
      CheckForDeath(instigator);
      valueChange.Invoke(this);
    }

    private void CheckForDeath(GameObject instigator)
    {
      if (currentHealth.value == 0 && !isDead)
      {
        HandleDeath();
        if (PlayerInfo.GetPlayer() == gameObject)
        {
          FindObjectOfType<SavingSystem>().Save("CompleteData", SaveType.All);
          FindObjectOfType<DeathScreen>().Show();
        }
        else
        {
          SoulEnergy energy = instigator.GetComponent<SoulEnergy>();
          EmitLoot(energy.GetSoulEnergyLevel());
          EmitExperience(instigator, energy.GetSoulEnergyLevel());
          energy.AddKill();
        }
      }
    }

    private void EmitLoot(float soulEnergy)
    {
      LootGenerator.instance.DropLoot(transform.position, soulEnergy);
    }

    public void LevelUpHealth(CharacterStats stats)
    {
      maxHealth.value = stats.GetStat(Stat.Health);
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

    private void UpdateMaxHealth(CharacterStats stats)
    {
      maxHealth.value = stats.GetStat(Stat.Health);
      currentHealth.value = maxHealth.value;
      valueChange.Invoke(this);
    }

    private void EmitExperience(GameObject instigator, float soulEnergy)
    {
      Experience playerExperience = instigator.GetComponent<Experience>();
      if (playerExperience)
      {
        CharacterStats stats = GetComponent<CharacterStats>();
        playerExperience.GainExperience((int)stats.GetStat(Stat.Experience), stats.Level, soulEnergy);
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
      NavMeshAgent agent;
      if (TryGetComponent(out agent)) agent.enabled = false;
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

    public void Interact(GameObject interacter) { }

    public GameObject GetGameObject()
    {
      return gameObject;
    }
  }
}
