using RPG.Core;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
  [SerializeField] GameObject player;

  private void LateUpdate()
  {
    transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
  }
}
