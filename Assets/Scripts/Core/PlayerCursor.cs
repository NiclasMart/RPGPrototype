using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Core
{
  public class PlayerCursor : MonoBehaviour
  {
    public enum CursorType
    {
      COMBAT,
      INTERACT,
      STANDARD,
      UI
    }

    [System.Serializable]
    class CursorMapping
    {
      public CursorType type;
      public Texture2D icon;
    }

    [SerializeField] RawImage cursor;
    [SerializeField] CursorMapping[] cursorMap;
    Camera cam;
    IInteraction target;
    public CursorType currentState = CursorType.STANDARD;
    public IInteraction Target { get => target; }
    Vector3 hitPosition;
    public Vector3 Position { get => hitPosition; }
    [HideInInspector] public bool hasRaycastHit;
    [HideInInspector] public bool active = true;

    private void Start()
    {
      Cursor.visible = false;
      cam = PlayerInfo.GetMainCamera();
    }

    private void Update()
    {
      if (!active)
      {
        DisableOldOutline();
        return;
      }

      SetCursorPosition();
      CheckForTargetable();
    }

    public void SetCursor(CursorType type)
    {
      CursorMapping cursorData = GetCursorMapping(type);
      cursor.texture = cursorData.icon;
      currentState = type;
    }

    public void ResetTarget()
    {
      if (lastTarget == target) lastTarget = null;
      target = null;
    }

    private void SetCursorPosition()
    {
      cursor.transform.position = Input.mousePosition;
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
      foreach (CursorMapping cursor in cursorMap)
      {
        if (cursor.type == type) return cursor;
      }
      return cursorMap[0];
    }

    IInteraction lastTarget;
    private void CheckForTargetable()
    {
      if (EventSystem.current.IsPointerOverGameObject())
      {
        SetCursor(CursorType.UI);
        hasRaycastHit = false;
        return;
      }

      RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
      SetMovePosition(hits);

      IInteraction closestHit = SearchClosestTarget(hits);

      if (closestHit != null)
      {
        HandleOutline(closestHit);
        lastTarget = closestHit;
        target = closestHit;
      }
      else
      {
        DisableOldOutline();
        target = null;
      }

    }

    private IInteraction SearchClosestTarget(RaycastHit[] hits)
    {
      IInteraction closestHit = null;
      float closestDistance = Mathf.Infinity;
      foreach (RaycastHit hit in hits)
      {
        if (hit.transform.CompareTag("Player")) continue;

        IInteraction cursorTarget = hit.transform.GetComponent<IInteraction>();
        if (cursorTarget != null)
        {
          float distance = Vector3.Distance(hit.point, hit.transform.position);
          if (distance < closestDistance)
          {
            closestHit = cursorTarget;
            closestDistance = distance;
          }
        }
      }

      return closestHit;
    }

    private void HandleOutline(IInteraction cursorTarget)
    {
      if (lastTarget != cursorTarget)
      {
        DisableOldOutline();

        //enable new outline
        var outline = cursorTarget.GetGameObject().GetComponent<Outline>();
        if (outline) outline.enabled = true;
      }
    }

    private void DisableOldOutline()
    {
      if (lastTarget == null) return;

      var oldOutline = lastTarget.GetGameObject().GetComponent<Outline>();
      if (oldOutline) oldOutline.enabled = false;

      lastTarget = null;
    }

    private void SetMovePosition(RaycastHit[] hits)
    {
      if (hits.Length != 0)
      {
        hasRaycastHit = true;
        hitPosition = hits[0].point;
      }
      else
      {
        hasRaycastHit = false;
      }
    }

    private Ray GetMouseRay()
    {
      return cam.ScreenPointToRay(Input.mousePosition);
    }
  }
}
