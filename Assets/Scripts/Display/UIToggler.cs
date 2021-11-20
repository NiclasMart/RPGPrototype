using UltEvents;
using UnityEngine;

namespace RPG.Display
{
  public class UIToggler : MonoBehaviour
  {
    [SerializeField] KeyCode displayButton = KeyCode.I;
    [SerializeField] UltEvent openAction, closeAction;
    bool displayActive = false;
    GameObject handletUI;

    private void Start()
    {
      handletUI = transform.GetChild(0).gameObject;
      handletUI.SetActive(false);      
    }

    private void Update()
    {
      ToggleDisplay();
    }

    private void ToggleDisplay()
    {
      if (Input.GetKeyDown(displayButton))
      {
        displayActive = !handletUI.activeSelf;
        handletUI.SetActive(displayActive);

        if (displayActive) openAction.Invoke();
        else closeAction.Invoke();
      }
    }
  }
}