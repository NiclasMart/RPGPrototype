using UnityEngine;
using RPG.Combat;

namespace RPG.Item
{
  public class Pickup : MonoBehaviour
  {
    [SerializeField] Weapon item;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
      {
        other.GetComponent<Fighter>().EquipWeapon(item);
        Destroy(gameObject);
      }
    }
  }
}
