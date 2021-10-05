using RPG.Core;
using UnityEngine;

namespace RPG.Display
{
  public class EnemyHUD : MonoBehaviour
  {
    [Header("Enemy")]
    [SerializeField] ResourceBar enemyLifeBar;
    [SerializeField] ValueDisplay enemyLevelDisplay;

    PlayerCursor cursor;
    IDisplayable enemyHealth;
    IDisplayable enemyLevel;

    private void Start() 
    {
      cursor = PlayerInfo.GetPlayerCursor();
    }
      
    private void Update() 
    {
      // if (cursor.Target != null)
      // {
      //  if (cursor.Target.GetGameObject().CompareTag("Enemy"))
      //  {
      //     enemyLifeBar.UpdateUI(enemyHealth);
      //  }
      // }
      // else SetUpEnemyDisplay
      
    }

    public void SetUpEnemyDisplay(IDisplayable health, IDisplayable level)
    {
      enemyHealth = health;
      enemyLevel = level;
      enemyLevelDisplay.UpdateUI(level);
    }
  }
}
