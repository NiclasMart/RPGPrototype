using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    public int portalIndex;
    [SerializeField] int connectedPortelIndex;
    [SerializeField] string loadSceneName = "";
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
      {
        StartCoroutine(Teleport());
      }
    }

    IEnumerator Teleport()
    {
      DontDestroyOnLoad(transform.parent.gameObject);
      yield return SceneManager.LoadSceneAsync(loadSceneName);

      Portal conectedPortal = GetConectedPortal();
      if (conectedPortal == null)
      {
        Debug.LogError("Can't find portal index within scene. Please try to ensure that all portal indicies are set up correctly!");
        yield return null;
      }
      UpdatePlayerSpawn(conectedPortal);

      Destroy(transform.parent.gameObject);
    }

    private void UpdatePlayerSpawn(Portal portal)
    {
      GameObject player = GameObject.FindGameObjectWithTag("Player");
      player.transform.position = portal.spawnPoint.position;
      player.transform.rotation = portal.spawnPoint.rotation;
    }

    private Portal GetConectedPortal()
    {
      Portal connectedPortal = null;
      PortalManager manager = GetNewScenePortalManager();

      if (manager != null) connectedPortal = manager.GetPortal(connectedPortelIndex);

      return connectedPortal;
    }

    private PortalManager GetNewScenePortalManager()
    {
      PortalManager[] managers = FindObjectsOfType<PortalManager>();
      foreach (PortalManager manager in managers)
      {
        if (manager.gameObject == transform.parent.gameObject) continue;

        return manager;
      }
      return null;
    }
  }
}