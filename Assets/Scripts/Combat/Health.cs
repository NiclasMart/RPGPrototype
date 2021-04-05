using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
  {
    [SerializeField] float startHealth = 100f;
    float currentHealth;

    private void Start()
    {
      currentHealth = startHealth;
    }

    public void ApplyDamage(float damage)
    {
      currentHealth = Mathf.Max(0, currentHealth - damage);
      print("current Health: " + currentHealth);
    }
  }
}
