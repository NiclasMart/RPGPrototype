using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
  public class SceneLoader : MonoBehaviour
  {
    public void LoadNewScene(string name){
      SceneManager.LoadScene(name);
    }
  }
}
