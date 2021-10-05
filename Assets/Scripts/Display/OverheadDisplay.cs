using UnityEngine;
using TMPro;
using System;
using RPG.Stats;

namespace RPG.Display
{
  public class OverheadDisplay : ResourceBar
  {
    [SerializeField] TextMeshProUGUI plainLevel;
    [SerializeField] GameObject lifebar;
    [SerializeField] CharacterStats characterLevel;
    Canvas canvas;

    private void Awake()
    {
      canvas = transform.parent.GetComponent<Canvas>();
    }

    private void Start()
    {
      SetLevelDisplay();
      EnableLifebar(false);
    }

    private void SetLevelDisplay()
    {
      plainLevel.text = characterLevel.Level.ToString();
      lifebar.GetComponentInChildren<TextMeshProUGUI>().text = characterLevel.Level.ToString();
    }

    public void Show(IDisplayable value)
    {
      if (value.GetCurrentValue() < value.GetMaxValue()) EnableLifebar(true);
      if (value.GetCurrentValue() <= 0) canvas.enabled = false;
      base.UpdateUI(value);
    }

    private void EnableLifebar(bool state)
    {
      plainLevel.transform.parent.gameObject.SetActive(!state);
      lifebar.SetActive(state);
    }
  }
}