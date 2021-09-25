using RPG.Core;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
  [SerializeField] GameObject player;
  [SerializeField] Vector2 distanceBounds;
  [SerializeField] float stdMinimapDistance;
  [SerializeField] float scrollSpeed = 0.1f;
  Camera activeMapCam;
  float currentMapDistance;

  private void Start()
  {
    activeMapCam = GetComponent<Camera>();
    activeMapCam.orthographicSize = stdMinimapDistance;
  }

  private void LateUpdate()
  {
    transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
  }

  public void UpdateCameraScroll(float delta)
  {
    float camSize = activeMapCam.orthographicSize;
    camSize += delta * scrollSpeed;
    camSize = Mathf.Min(distanceBounds.y, Mathf.Max(distanceBounds.x, camSize));
    activeMapCam.orthographicSize = camSize;
    currentMapDistance = camSize;
  }

  public void SwitchCamera(bool isLargeMap)
  {
    activeMapCam.orthographicSize = isLargeMap ? currentMapDistance : stdMinimapDistance;
  }
}
