using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuOption : MonoBehaviour
{
    public UnityEvent OnTrigger;


    /// Functions ///

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /// Collisions ///

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            OnTrigger.Invoke();
        }
    }

}
