using System.Collections;
using RPG.Core;
using RPG.Interaction;
using UnityEngine;


namespace RPG.Items
{
  public class Pickup : Interactable
  {
    [SerializeField]
    Gradient[] gradients;
    public Item item;

    public void Spawn(Item item)
    {
      this.item = item;

      GameObject dropedItem = InstanciateDropedItem(item);
      SetTransform(dropedItem);
      SetEffectColors();
      StartCoroutine(EnableOutline());
    }

    private GameObject InstanciateDropedItem(Item item)
    {
      bool hasChild = item.itemObject.transform.childCount != 0;
      GameObject itemObject = hasChild ? item.itemObject.transform.GetChild(0).gameObject : item.itemObject;
      GameObject dropedItem = Instantiate(itemObject, transform);
      return dropedItem;
    }

    private static void SetTransform(GameObject dropedItem)
    {
      dropedItem.transform.rotation = Quaternion.identity;
      dropedItem.transform.localPosition = Vector3.zero;
      dropedItem.transform.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
    }
    
    private void SetEffectColors()
    {
      LineRenderer lineRenderer = GetComponentInChildren<LineRenderer>();
      lineRenderer.colorGradient = gradients[(int)item.rarity];
      lineRenderer.SetPosition(1, new Vector3(0, 3 + 2 * (int)item.rarity, 0));

      var particleData = GetComponentInChildren<ParticleSystem>().main;
      particleData.startColor = gradients[(int)item.rarity];
    }

    public void Take()
    {
      LootSplitter.FreeGridPosition(transform.position);
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
