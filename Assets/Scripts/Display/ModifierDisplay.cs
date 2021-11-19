using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using RPG.Interaction;

namespace RPG.Display
{
  public class ModifierDisplay : MonoBehaviour
  {
    [SerializeField] List<ModifierShowPanel> panels = new List<ModifierShowPanel>();
    [SerializeField] RectTransform canvasRect;
    [SerializeField] Image panelBackground, arrow;
    PlayerInventory inventory;

    int activeConnections = 0;
    bool compareModeIsActive = false;
    Vector3 panelStartPosition;

    private void Awake()
    {
      inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
      panelStartPosition = panels[1].GetComponent<RectTransform>().position;
      SetUIActive(false);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Tab)) SetCompareMode();
    }

    private void SetCompareMode()
    {
      if (compareModeIsActive)
      {
        compareModeIsActive = false;
        SetUIActive(false);
      }
      else if (activeConnections > 0)
      {
        panels[1].GetComponent<RectTransform>().position = panelStartPosition;
        compareModeIsActive = true;
        SetUIActive(true);
      }
    }

    public void ShowModifiers(Item item, RectTransform rectTransform)
    {
      ModifiableItem modItem = item as ModifiableItem;
      if (modItem == null) return;

      if (compareModeIsActive)
      {
        ModifiableItem equipedItem = inventory.GetEquipedItem(item.itemType) as ModifiableItem;
        panels[0].DisplayItem(equipedItem);
      }
      else
      {
        panels[1].SetActive(true);
        SetPosition(rectTransform);
      }
      panels[1].DisplayItem(modItem);
    }

    private void SetPosition(RectTransform slotTransform)
    {
      panels[1].transform.position = slotTransform.position - new Vector3(slotTransform.rect.width / 2, 0, 0);
      // if (Input.mousePosition.x > canvasRect.rect.width / 2) panels[1].transform.position = Input.mousePosition - new Vector3(panels[1].GetComponent<RectTransform>().rect.width, 0, 0);
      // else panels[1].transform.position = Input.mousePosition;
    }


    public void HideModifiers()
    {
      if (compareModeIsActive)
      {
        panels[0].Clear();
        panels[1].Clear();
        return;
      }
      panels[1].gameObject.SetActive(false);
    }

    public void RegisterMenu()
    {
      activeConnections++;
    }

    public void UnregisterMenu()
    {
      activeConnections--;
      if (activeConnections == 0) SetUIActive(false);
    }


    public void SetUIActive(bool active)
    {
      panelBackground.gameObject.SetActive(active);
      arrow.gameObject.SetActive(active);
      foreach (var panel in panels)
      {
        panel.gameObject.SetActive(active);
      }
    }


  }
}
