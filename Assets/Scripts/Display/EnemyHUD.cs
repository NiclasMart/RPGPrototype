using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class EnemyHUD : MonoBehaviour
  {
    [Header("Enemy")]
    [SerializeField] ResourceBar enemyLifeBar;
    [SerializeField] ValueDisplay enemyLevelDisplay;

    IDisplayable enemyHealth;
    IDisplayable enemyLevel;
    private void Update() {
      enemyLifeBar.UpdateUI(enemyHealth);
    }

    public void SetUpEnemyDisplay(IDisplayable health, IDisplayable level)
    {
      enemyHealth = health;
      enemyLevel = level;
      enemyLevelDisplay.UpdateUI(level);
    }
  }
}
