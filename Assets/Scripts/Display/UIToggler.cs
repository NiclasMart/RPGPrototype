using UnityEngine;

namespace RPG.Display
{  
  public class UIToggler : MonoBehaviour 
  {
    [SerializeField] KeyCode displayButton = KeyCode.I;
    bool displayActive = false;

    private void Awake()
    {
      transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
      ToggleDisplay();
    }

    private void ToggleDisplay()
    {
      if (Input.GetKeyDown(displayButton))
      {
        displayActive = !displayActive;
        transform.GetChild(0).gameObject.SetActive(displayActive);
      }
    }
  }
}