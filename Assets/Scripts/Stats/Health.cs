using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Display;
using GameDevTV.Utils;
using RPG.Items;
using RPG.Saving;
using Display;
using UnityEngine.SceneManagement;

namespace RPG.Stats
{
  public class Health : MonoBehaviour, IDisplayable, IInteraction, ISaveable
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
    public AlterValue<float> onTakeDamage;

    private void Awake()
    {
      animator = GetComponentInChildren<Animator>();
      maxHealth = new LazyValue<float>(GetInitializeHealth);
      if (currentHealth == null) currentHealth = new LazyValue<float>(GetInitializeHealth);

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

    public bool ApplyDamage(GameObject instigator, float damage)
    {
      onTakeDamage?.Invoke(ref damage);
      currentHealth.value = Mathf.Max(0, currentHealth.value - damage);
      valueChange.Invoke(this);
      return CheckForDeath(instigator);
    }

    private bool CheckForDeath(GameObject instigator)
    {
      if (currentHealth.value == 0 && !isDead)
      {
        HandleDeath();
        if (PlayerInfo.GetPlayer() == gameObject)
        {
          FindObjectOfType<SavingSystem>().Save("PlayerData", SaveType.All);
          FindObjectOfType<DeathScreen>().Show();
        }
        else
        {
          SoulEnergy energy = instigator.GetComponent<SoulEnergy>();
          EmitLoot(energy.GetSoulEnergyLevel());
          EmitExperience(instigator, energy.GetSoulEnergyLevel());
          energy.AddKill();
        }
        return true;
      }
      return false;
    }

    private void EmitLoot(float soulEnergy)
    {
      LootGenerator.instance.DropLoot(transform.position, soulEnergy);
    }

    public void LevelUpHealth(CharacterStats stats)
    {
      maxHealth.value = stats.GetStat(Stat.Health);
      HealPercentageMax(lvlUpHeal);
    }

    public void HealAbsolut(int value)
    {
      value = Mathf.Abs(value);
      currentHealth.value = Mathf.Min(maxHealth.value, currentHealth.value + value);
      valueChange.Invoke(this);
    }

    public void HealPercentageCurrent(float percent)
    {
      percent = Mathf.Abs(percent);
      currentHealth.value = Mathf.Min(maxHealth.value, currentHealth.value += currentHealth.value * percent);
      valueChange.Invoke(this);
    }

    public void HealPercentageMax(float percent)
    {
      percent = Mathf.Abs(percent);
      currentHealth.value = Mathf.Min(maxHealth.value, currentHealth.value += maxHealth.value * percent);
      valueChange.Invoke(this);
    }

    private void UpdateMaxHealth(CharacterStats stats)
    {
      maxHealth.value = stats.GetStat(Stat.Health);
      string sceneName = SceneManager.GetActiveScene().name;
      if (sceneName == "TransitionRoom" || sceneName == "Village") currentHealth.value = maxHealth.value;
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

    public object CaptureSaveData(SaveType saveType)
    {
      return currentHealth.value;
    }

    public void RestoreSaveData(object data)
    {
      string sceneName = SceneManager.GetActiveScene().name;
      if (sceneName == "TransitionRoom" || sceneName == "Village") return;
      currentHealth = new LazyValue<float>(() => (float)data);
    }
  }
}
