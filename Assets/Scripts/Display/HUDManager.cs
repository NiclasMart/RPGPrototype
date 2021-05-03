using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class HUDManager : MonoBehaviour
  {
    [Header("Player")]
    [SerializeField] ResourceBar playerLifeBar;
    [SerializeField] ResourceBar experienceBar;
    [SerializeField] ValueDisplay playerLevel;

    [Header("Enemy")]
    [SerializeField] ResourceBar enemyLifeBar;
    [SerializeField] ValueDisplay enemyLevel;

    public void SetUpPlayerHealthBar(IDisplayable values)
    {
      playerLifeBar.ConnectElement(values);
    }

    public void SetUpExperienceBar(IDisplayable values)
    {
      experienceBar.ConnectElement(values);
    }

    public void SetUpPlayerLevelDisplay(IDisplayable value)
    {
      playerLevel.ConnectElement(value);
    }

    public void SetUpEnemyDisplay(IDisplayable health, IDisplayable level)
    {
      
      enemyLifeBar.ConnectElement(health);
      enemyLevel.ConnectElement(level);
    }
  }
}
