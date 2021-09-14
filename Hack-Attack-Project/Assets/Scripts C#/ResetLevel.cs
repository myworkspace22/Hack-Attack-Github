using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void Loadscene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Reloadscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
}
