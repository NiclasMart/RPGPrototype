using RPG.Items;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Display
{
  public class ModifierShowPanel : MonoBehaviour
  {
    [SerializeField] Transform mainStats, modifiers;
    [SerializeField] Image icon;
    Color epicCol, legendaryCol;

    public void DisplayModifiers(ModifiableItem item)
    {

    }
  }
}