using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class ModifierShowPanel : MonoBehaviour
  {
    [SerializeField] TextMeshProUGUI title, mainStats;
    [SerializeField] Transform modifiers;
    [SerializeField] Image icon;
    Color epicCol, legendaryCol;

    public void DisplayModifiers(ModifiableItem item)
    {
      Clear();

      title.color = ModifiableItem.GetRarityColor(item.rarity);
      title.text = item.GetTitleText();

      mainStats.color = ModifiableItem.GetRarityColor(Rank.Normal);
      mainStats.text = item.GetMainStatText();

      TextMeshProUGUI[] fields = modifiers.GetComponentsInChildren<TextMeshProUGUI>();
      for (int i = 0; i < item.modifiers.Count; i++)
      {
        ModifiableItem.Modifier modifier = item.modifiers[i];
        fields[i].text = modifier.GetDisplayText();

        fields[i].color = ModifiableItem.GetRarityColor(modifier.rarity);
      }
    }

    public void SetActive(bool active)
    {
      gameObject.SetActive(active);
    }

    public void Clear()
    {
      foreach (var display in modifiers.GetComponentsInChildren<TextMeshProUGUI>())
      {
        display.text = "";
      }


    }
  }
}