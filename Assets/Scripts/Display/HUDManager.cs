using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class HUDManager : MonoBehaviour
  {
    [SerializeField] ResourceBar playerLifeBar;
    [SerializeField] ResourceBar experienceBar;
    [SerializeField] ResourceBar enemyLifeBar;


    private void Start()
    {
      enemyLifeBar.SetVisible(false);
    }

    public void SetUpPlayerHealthBar(IDisplayable values)
    {
      playerLifeBar.ConnectBar(values);
    }

    public void SetUpExperienceBar(IDisplayable values)
    {
      experienceBar.ConnectBar(values);
    }

    public void SetUpEnemyHealthBar(IDisplayable values)
    {
      enemyLifeBar.SetVisible(true);
      enemyLifeBar.ConnectBar(values);
    }

    public void HideEnemyHealthBar()
    {
      enemyLifeBar.SetVisible(false);
      enemyLifeBar.ConnectBar(null);
    }
  }
}
