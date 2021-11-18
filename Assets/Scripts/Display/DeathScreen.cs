using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Display
{
  public class DeathScreen : MonoBehaviour
  {
    public void Show()
    {
      transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Revive()
    {
      SceneManager.LoadScene("Village");
    }
  }
}
