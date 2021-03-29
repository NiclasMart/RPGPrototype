using UnityEngine;

namespace RPG.Combat
{
  public class Fighter : MonoBehaviour
  {
    public void Attack(Attackable target)
    {
      print("I will take all of your LOOT! YOU dumbass " + target.name);
    }
  }
}