using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using TMPro;

namespace RPG.Display
{
  public class SoulEnergyDisplay : MonoBehaviour
  {
    [SerializeField] SoulEnergy soulEnergy;
    [SerializeField] TextMeshProUGUI xpIncreasementDisplay, dropChanceDisplay, dropRarityDisplay;
    public void ToggleDisplay(bool on)
    {
      if (on) SetValues();
      gameObject.SetActive(on);
    }

    private void SetValues()
    {
      float soulEnergyLevel = soulEnergy.GetSoulEnergyLevel();
      float xpMultiplier = Mathf.Lerp(0, 1, soulEnergyLevel) * 100;
      float dropChance = soulEnergyLevel * 100;
      float dropRarity = Mathf.Lerp(0, 0.2f, soulEnergyLevel) * 100;

      xpIncreasementDisplay.text = $"+{xpMultiplier.ToString("F1")}% Experience Gain";
      dropChanceDisplay.text = $"+{dropChance.ToString("F1")}% increased drop chance";
      dropRarityDisplay.text = $"+{dropRarity.ToString("F1")}% increased drop rarity";

    }
  }
}
