using System.Collections;
using RPG.Core;
using UnityEngine;


namespace RPG.Interaction
{
  public class Pickup : Interactable
  {
    public Item item;

    public void Initialize(Item item)
    {
      this.item = item;
      GameObject itemObject = Instantiate(item.itemObject, transform);
      Transform child = itemObject.transform.GetChild(0);
      itemObject.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
      StartCoroutine(EnableOutline());
    }

    public void Take()
    {
      //player.GetComponent<Fighter>().EquipWeapon(item);
      Destroy(gameObject);
      PlayerInfo.GetPlayerCursor().ResetTarget();
      print("resetTarget");
    }

    public override void Interact(GameObject interacter)
    {
      SimpleInventory inventory = interacter.GetComponent<Interacter>().inventory;
      if (!inventory) return;

      if (inventory.CheckCapacity(item.weight))
      {
        inventory.AddItem(item);
        Take();
      }
      else
      {
        //show full effect
      }
    }

    IEnumerator EnableOutline()
    {
      yield return new WaitForEndOfFrame();
      GetComponent<Outline>().Initialize();
    }
  }
}
