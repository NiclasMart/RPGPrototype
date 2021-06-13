using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class ModifierDisplay : MonoBehaviour
  {
    CanvasGroup graficComponent;
    TextMeshProUGUI display;

    private void Awake()
    {
      graficComponent = GetComponent<CanvasGroup>();
      display = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
      SetDisplay(false);
    }

    public void ShowModifiers(Item item)
    {
      ModifiableItem modItem = item as ModifiableItem;
      if (modItem == null) return;

      SetText(modItem);
      transform.position = Input.mousePosition;
      SetDisplay(true);
    }

    private void SetText(ModifiableItem modItem)
    {
      display.text = "";
      foreach (var modifier in modItem.modifiers)
      {
        display.text += modifier.GetDisplayText() + "\n";
      }
    }

    public void HideModifiers()
    {
      Debug.Log("Hide Modifiers");
      SetDisplay(false);
    }

    private void SetDisplay(bool active)
    {
      if (active) graficComponent.alpha = 1f;
      else graficComponent.alpha = 0;
    }
  }
}
