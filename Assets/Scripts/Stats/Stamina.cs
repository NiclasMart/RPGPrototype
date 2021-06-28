using System;
using GameDevTV.Utils;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class Stamina : MonoBehaviour, IDisplayable
  {
    [SerializeField] float regenerationTickTime = 0.5f;
    LazyValue<float> maxStamina;
    LazyValue<float> currentStamina;

    CharacterStats stats;

    public ValueChangeEvent valueChange;

    private void Awake()
    {
      maxStamina = new LazyValue<float>(GetInitializeStamina);
      currentStamina = new LazyValue<float>(GetInitializeStamina);

      stats = GetComponent<CharacterStats>();
      stats.statsChange += UpdateMaxStamina;
    }

    private void Start()
    {
      maxStamina.ForceInit();
      currentStamina.ForceInit();
      valueChange.Invoke(this);
    }

    private void Update()
    {
      RegenerateStamina();
    }

    public bool UseStamina(float neededStamina)
    {
      if (!CheckStamina(neededStamina)) return false;

      currentStamina.value -= neededStamina;
      valueChange.Invoke(this);
      return true;
    }

    public void UpdateMaxStamina(CharacterStats stats)
    {
      maxStamina.value = stats.GetStat(Stat.Stamina);
      currentStamina.value = maxStamina.value;
      valueChange.Invoke(this);
    }

    float lastRegenTick = Mathf.NegativeInfinity;
    private void RegenerateStamina()
    {
      if (currentStamina.value >= maxStamina.value) return;
      if (lastRegenTick + regenerationTickTime > Time.time) return;

      lastRegenTick = Time.time;
      float regenRate = stats.GetStat(Stat.StaminaRegeneration);
      currentStamina.value = Mathf.Min(maxStamina.value, currentStamina.value + regenRate * regenerationTickTime);
      valueChange.Invoke(this);
    }

    private bool CheckStamina(float amount)
    {
      return amount <= currentStamina.value;
    }

    private float GetInitializeStamina()
    {
      return stats.GetStat(Stat.Stamina);
    }


    public float GetCurrentValue()
    {
      return currentStamina.value;
    }

    public float GetMaxValue()
    {
      return maxStamina.value;
    }
  }
}