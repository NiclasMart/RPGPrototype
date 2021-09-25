using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class MapUI : MonoBehaviour
  {
    [SerializeField] KeyCode mapToggleButton = KeyCode.M;
    [SerializeField] MinimapCamera cam;
    [SerializeField] GameObject minimap, largemap;

    bool largeMapIsActive = false;

    private void Start()
    {
      minimap.SetActive(true);
      largemap.SetActive(false);
    }

    private void Update()
    {
      HandleMapSwitch();

      if (largeMapIsActive) cam.UpdateCameraScroll(Input.mouseScrollDelta.y);

    }

    private void HandleMapSwitch()
    {
      if (Input.GetKeyDown(mapToggleButton))
      {
        minimap.SetActive(largeMapIsActive);
        largemap.SetActive(!largeMapIsActive);
        largeMapIsActive = !largeMapIsActive;
        cam.SwitchCamera(largeMapIsActive);
      }
    }
  }
}
