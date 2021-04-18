using UnityEngine;

namespace RPG.SceneManagement
{
  public class Portal : MonoBehaviour
  {
    [SerializeField] string sceneName = "";
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player"))
      {
        GetComponent<SceneLoader>().LoadNewScene(sceneName);
      }
    }
  }
}