using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
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

    private void Update()
    {
      Vector3 lookDirection = transform.position - cam.transform.position;
      canvas.transform.LookAt(transform.position + lookDirection);
    }
  }
}
