using UnityEngine;

namespace RPG.Core
{
  public class PlayerCursor : MonoBehaviour
  {
    public enum CursorType
    {
      COMBAT,
      INTERACT,
      MOVE
    }

    [System.Serializable]
    class CursorMapping
    {
      public CursorType type;
      public Texture2D icon;
      public Vector2 hotspot;
    }

    [SerializeField] CursorMapping[] cursorMap;
    Camera cam;
    Targetable target;
    public Targetable Target { get => target; }
    Vector3 hitPosition;
    public Vector3 Position { get => hitPosition; }
    [HideInInspector] public bool hasRaycastHit;

    private void Start()
    {
      cam = PlayerInfo.GetMainCamera();
    }

    private void Update()
    {
      CheckForTargetable();
    }

    public void SetCursor(CursorType type)
    {
      CursorMapping cursor = GetCursorMapping(type);
      Cursor.SetCursor(cursor.icon, cursor.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
      foreach (CursorMapping cursor in cursorMap)
      {
        if (cursor.type == type) return cursor;
      }
      return cursorMap[0];
    }

    Targetable lastTarget;
    private void CheckForTargetable()
    {
      RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
      CheckRaycastHitPoint(hits);

      foreach (RaycastHit hit in hits)
      {
        Targetable cursorTarget = hit.transform.GetComponent<Targetable>();
        if (cursorTarget != null)
        {
          HandleOutline(cursorTarget);
          target = cursorTarget;
          return;
        }
      }
      DisableOldOutline();
      target = null;
    }

    private void HandleOutline(Targetable cursorTarget)
    {
      if (lastTarget != cursorTarget)
      {
        DisableOldOutline();

        //enable new outline
        var outline = cursorTarget.GetComponent<Outline>();
        if (outline) outline.enabled = true;

        lastTarget = cursorTarget;
      }
    }

    private void DisableOldOutline()
    {
      if (!lastTarget) return;

      var oldOutline = lastTarget.GetComponent<Outline>();
      if (oldOutline) oldOutline.enabled = false;

      lastTarget = null;
    }

    private void CheckRaycastHitPoint(RaycastHit[] hits)
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
