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
      LOOT,
      STANDARD,
      UI,
      ENTER,
      USE
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
      HitData hitData = EvaluateHits(hits);
      SetMovePosition(hitData.cursorPosition);

      if (hitData.cursorTarget != null)
      {
        HandleOutline(hitData.cursorTarget);
        lastTarget = hitData.cursorTarget;
        target = hitData.cursorTarget;
      }
      else
      {
        DisableOldOutline();
        target = null;
      }

    }

    public struct HitData
    {
      public Vector3 cursorPosition;
      public IInteraction cursorTarget;
    }

    private HitData EvaluateHits(RaycastHit[] hits)
    {
      HitData data = new HitData();
      float closestTargetDistance = Mathf.Infinity, closestCursorPosition = Mathf.NegativeInfinity;
      foreach (RaycastHit hit in hits)
      {
        if (hit.transform.CompareTag("Player")) continue;

        //get cursorPosition
        if (closestCursorPosition < hit.point.y)
        {
          data.cursorPosition = hit.point;
          closestCursorPosition = hit.point.y;
        }

        //get InteractionTarget
        IInteraction tmpHit = hit.transform.GetComponent<IInteraction>();
        if (tmpHit != null)
        {
          float distance = Vector3.Distance(hit.point, hit.transform.position);
          if (distance < closestTargetDistance)
          {
            data.cursorTarget = tmpHit;
            closestTargetDistance = distance;
          }
        }
      }

      return data;
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

    private void SetMovePosition(Vector3 position)
    {
      if (position != null)
      {
        hasRaycastHit = true;
        hitPosition = position;
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
