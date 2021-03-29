using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    Mover mover;
    Camera cam;

    private void Start()
    {
      mover = GetComponent<Mover>();
      cam = Camera.main;
    }

    private void Update()
    {
      if (Input.GetMouseButton(0))
      {
        CalculateNextMovementPosition();
      }
    }

    private void CalculateNextMovementPosition()
    {
      Ray ray = cam.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        mover.MoveTo(hit.point);
      }
    }
  }
}
