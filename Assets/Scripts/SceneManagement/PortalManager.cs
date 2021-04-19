using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class PortalManager : MonoBehaviour
  {
    //[HideInInspector] 
    public List<Portal> portals;
    public int identifier;

    private void Awake()
    {
      FindAllPortals();

    }

    public Portal GetPortal(int index)
    {
      foreach (Portal portal in portals)
      {
        if (portal.portalIndex == index) return portal;
      }
      return null;
    }

    private void FindAllPortals()
    {
      foreach (Portal portal in transform.GetComponentsInChildren<Portal>())
      {
        portals.Add(portal);
      }
    }
  }
}