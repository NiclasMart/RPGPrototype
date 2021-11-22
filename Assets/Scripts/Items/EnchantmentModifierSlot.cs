using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RPG.Items
{
  public class EnchantmentModifierSlot : MonoBehaviour
  {
    ModifiableItem.Modifier modifier;
    TextMeshProUGUI label;
    Image btn;
    [HideInInspector] public Color defaultColor, selectColor;

    private void Awake()
    {
      btn = GetComponent<Image>();
      label = GetComponentInChildren<TextMeshProUGUI>();
      defaultColor = btn.color;
    }

    public void SetModifier(ModifiableItem.Modifier mod)
    {
      modifier = mod;
      label.text = mod.GetDisplayText();
      label.color = ModifiableItem.GetRarityColor(mod.rarity);

    }

    public ModifiableItem.Modifier GetModifier()
    {
      return modifier;
    }

    public void ResetSlot()
    {
      label.text = "---";
      label.color = Color.white;
      modifier = null;
      btn.color = defaultColor;
    }

    public void Select()
    {
      btn.color = selectColor;
    }

    public void Deselect()
    {
      btn.color = defaultColor;
    }

  }
}
