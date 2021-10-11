using System.Collections.Generic;
using RPG.Items;
using UnityEngine;


namespace RPG.Saving
{
  [CreateAssetMenu(fileName = "SceneTransferData", menuName = "SceneTransferData", order = 0)]
  public class SceneTransferData : ScriptableObject
  {
      int kills;
      int experience;
      List<Item> items = new List<Item>();

  }

}