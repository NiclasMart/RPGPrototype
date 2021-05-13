using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Display
{
  public class Billboard : MonoBehaviour
  {
    Transform canvas;
    Camera cam;

    private void Awake()
    {
      canvas = GetComponent<Transform>();
    }

    private void Start()
    {
      cam = PlayerInfo.GetMainCamera();
      canvas.GetComponent<Canvas>().worldCamera = cam;
    }

    private void LateUpdate()
    {
      Vector3 lookDirection = transform.position - cam.transform.position;
      canvas.transform.LookAt(transform.position + lookDirection);
    }
  }
}
