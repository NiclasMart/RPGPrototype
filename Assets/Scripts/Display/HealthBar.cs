using System;
using RPG.Resources;
using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] protected Slider slider;
    [SerializeField] protected Image fill;
    [SerializeField] protected TextMeshProUGUI currentHealth;
    [SerializeField] TextMeshProUGUI maxHealth;

    [SerializeField] protected Health health;
    protected BaseStats stats;

    private void Start()
    {
      if (health) InitializeHealthBar();
      else gameObject.SetActive(false);
    }

    private void Update()
    {
      if (health) UpdateHealthDisplay();
    }

    public void SetHealthDisplay(Health health, bool show)
    {
      gameObject.SetActive(show);
      this.health = health;

      if (!health) return;
      InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
      stats = health.gameObject.GetComponent<BaseStats>();
      float baseHealth = stats.GetHealth();
      slider.maxValue = baseHealth;
      slider.value = baseHealth;
      if (maxHealth) maxHealth.text = Mathf.Floor(baseHealth).ToString();
    }

    protected void UpdateHealthDisplay()
    {
      slider.value = health.CurrentHealth;
      currentHealth.text = Mathf.Floor(health.CurrentHealth).ToString();
    }


  }
}