using System.Collections;
using System.Collections.Generic;
using RPG.Items;
using UnityEngine;

public class DebugDropLoot : MonoBehaviour
{
  private void Start()
  {
    GetComponent<LootGenerator>().DropLoot(transform.position, 0);
  }

}
