using UltEvents;
using UnityEngine;

namespace RPG.Display
{
  public class UIToggler : MonoBehaviour
  {
    [SerializeField] KeyCode displayButton = KeyCode.I;
    [SerializeField] UltEvent openAction, closeAction;
    bool displayActive = false;

    private void Start()
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

        if (displayActive) openAction.Invoke();
        else closeAction.Invoke();
      }
    }
  }
}