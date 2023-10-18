using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class sceneManager 
{
    public static string currentScene;
    public static string sceneToLoad;
    public static void exitGame()
    {
        Application.Quit();
    }
    public static void reloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
