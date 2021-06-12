using System.Collections;
using RPG.Core;
using RPG.Interaction;
using UnityEngine;


namespace RPG.Items
{
  public class Pickup : Interactable
  {
    public Item item;

    public void Initialize(Item item)
    {
      this.item = item;
      GameObject itemObject = Instantiate(item.itemObject.transform.GetChild(0).gameObject, transform);
      itemObject.transform.rotation = Quaternion.identity;
      itemObject.transform.localPosition = Vector3.zero;
      itemObject.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
      StartCoroutine(EnableOutline());
    }

    public void Take()
    {
      Destroy(gameObject);
      PlayerInfo.GetPlayerCursor().ResetTarget();
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
