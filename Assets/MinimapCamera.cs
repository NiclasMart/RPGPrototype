using System;
using RPG.Core;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
  [SerializeField] GameObject player;
  [SerializeField] Vector2 distanceBounds;
  [SerializeField] float stdMinimapDistance;

  Camera cam;
  bool shouldFollowPlayer = true;
  float currentMapDistance;
  Vector3 currentMinMapCamPosition, currentLargeMapCamPosition;

  private void Start()
  {
    cam = GetComponent<Camera>();
    cam.orthographicSize = stdMinimapDistance;
    currentMinMapCamPosition = currentLargeMapCamPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
  }

  private void LateUpdate()
  {
    if (shouldFollowPlayer) transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
  }

  public void UpdateCameraScroll(float delta)
  {
    float camSize = cam.orthographicSize;
    camSize += delta;
    camSize = Mathf.Min(distanceBounds.y, Mathf.Max(distanceBounds.x, camSize));
    cam.orthographicSize = camSize;
    currentMapDistance = camSize;
  }

  internal void CenterCamera()
  {
    transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    currentLargeMapCamPosition = transform.position;
  }

  public void UpdateCameraPosition(Vector3 direction)
  {
    cam.transform.position += direction;
    currentLargeMapCamPosition = cam.transform.position;
  }

  public void SwitchCamera(bool isLargeMap)
  {
    cam.orthographicSize = isLargeMap ? currentMapDistance : stdMinimapDistance;
    cam.transform.position = isLargeMap ? currentLargeMapCamPosition : currentMinMapCamPosition;
    shouldFollowPlayer = !isLargeMap;
  }
}
