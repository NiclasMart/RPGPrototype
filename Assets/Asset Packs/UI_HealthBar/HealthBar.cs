using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int startHealth;
    [SerializeField] Gradient colorGradient;

    [SerializeField] Slider slider;
    [SerializeField] Image fill;

    int currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
        InitializeHealthBar();
    }

    private void InitializeHealthBar()
    {
        slider.maxValue = startHealth;
        slider.value = startHealth;

        fill.color = colorGradient.Evaluate(1f);
    }

    public bool ApplyDamage(int amount)
    {
        currentHealth -= Mathf.Abs(amount);
        UpdateHealthDisplay();

        return currentHealth > 0;
    }

    void UpdateHealthDisplay()
    {
        slider.value = currentHealth;

        fill.color = colorGradient.Evaluate(slider.normalizedValue);
    }
}
