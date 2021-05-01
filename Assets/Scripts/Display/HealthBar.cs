using RPG.Core;
using RPG.Resources;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] Slider slider;
    [SerializeField] Image fill;

    [SerializeField] TextMeshProUGUI currentHealth;
    [SerializeField] TextMeshProUGUI maxHealth;

    BaseStats stats;
    Health health;

    private void Start()
    {

      health = PlayerInfo.GetPlayer().GetComponent<Health>();
      stats = PlayerInfo.GetPlayer().GetComponent<BaseStats>();
      InitializeHealthBar();
    }

    private void Update()
    {
      UpdateHealthDisplay();
    }

    private void InitializeHealthBar()
    {
      float baseHealth = stats.GetHealth();
      slider.maxValue = baseHealth;
      slider.value = baseHealth;
      maxHealth.text = Mathf.Floor(baseHealth).ToString();
    }

    void UpdateHealthDisplay()
    {
      slider.value = health.CurrentHealth;
      currentHealth.text = Mathf.Floor(health.CurrentHealth).ToString();
    }
  }

}