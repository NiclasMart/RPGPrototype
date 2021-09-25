using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Display
{
  public class MapUI : MonoBehaviour
  {
    [SerializeField] KeyCode mapToggleButton = KeyCode.M;
    [SerializeField] float scrollSpeed = -2f, moveSpeed = 1f;
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

      if (largeMapIsActive)
      {
        cam.UpdateCameraScroll(Input.mouseScrollDelta.y * scrollSpeed);
        Vector3 mapMovement = GetInputMovement();
        cam.UpdateCameraPosition(mapMovement * moveSpeed);

        if (Input.GetMouseButtonDown(2)) cam.CenterCamera();
      }

    }

    private Vector3 GetInputMovement()
    {
      Vector3 movement = Vector3.zero;

      if (Input.GetKey(KeyCode.LeftArrow)) movement.x = -1;
      if (Input.GetKey(KeyCode.RightArrow)) movement.x = 1;
      if (Input.GetKey(KeyCode.UpArrow)) movement.z = 1;
      if (Input.GetKey(KeyCode.DownArrow)) movement.z = -1;

      return movement;
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
