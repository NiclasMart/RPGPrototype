using UnityEngine;


namespace RPG.Interaction
{
  public class Pickup : MonoBehaviour
  {
    public Item item;

    private void OnEnable() 
    {
      item.CreateID();
    }

    public void Take(/*GameObject player*/)
    {
      //player.GetComponent<Fighter>().EquipWeapon(item);
      Destroy(gameObject);
    }
  }
}
