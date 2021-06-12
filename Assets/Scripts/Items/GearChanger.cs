using RPG.Combat;
using UnityEngine;

namespace RPG.Items
{
  public class GearChanger : MonoBehaviour
  {
    public void EquipGear(Item item)
    {
      if (item as Weapon != null) GetComponent<Fighter>().EquipWeapon(item as Weapon);
    }
  }
}