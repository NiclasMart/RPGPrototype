using UnityEngine;
using RPG.Combat;

namespace RPG.Interaction
{
  public class Pickup : MonoBehaviour
  {
    [SerializeField] Weapon item;

    public void Take(GameObject player)
    {
      player.GetComponent<Fighter>().EquipWeapon(item);
      Destroy(gameObject);
    }
  }
}
