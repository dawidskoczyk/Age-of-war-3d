using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript: MonoBehaviour
{
    private int _activeSceneNumber;
    private Scene _scene;
    void Start()
    {
        _scene = SceneManager.GetActiveScene();
        _activeSceneNumber = _scene.buildIndex;
    }

    public void  LoadNextScene()
    {
        _activeSceneNumber++;
        print(_activeSceneNumber);
        LoadSceneNew();
    }
    public void LoadPreviousScene()
    {
        _activeSceneNumber--;
        LoadSceneNew();
    }
    public void LoadSceneNew()
    {
        print(SceneManager.GetSceneByBuildIndex(_activeSceneNumber).name + "load scene");
        SceneManager.LoadScene(_activeSceneNumber,LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
