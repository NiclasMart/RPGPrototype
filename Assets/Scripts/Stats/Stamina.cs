using GameDevTV.Utils;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class Stamina : MonoBehaviour, IDisplayable
  {
    LazyValue<float> maxStamina;
    LazyValue<float> currentStamina;

    public ValueChangeEvent valueChange;

    private void Awake()
    {
      maxStamina = new LazyValue<float>(GetInitializeStamina);
      currentStamina = new LazyValue<float>(GetInitializeStamina);

      GetComponent<CharacterStats>().statsChange += UpdateMaxStamina;
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

    private bool CheckStamina(float amount)
    {
      return amount <= currentStamina.value;
    }

    private float GetInitializeStamina()
    {
      return GetComponent<CharacterStats>().GetStat(Stat.Stamina);
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