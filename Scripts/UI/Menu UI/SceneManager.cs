using SM = UnityEngine.SceneManagement.SceneManager;

public static class SceneManager
{
    public static void NewScene(string targetScene)
    {
        SM.LoadSceneAsync(targetScene);
    }

    public static void SwapAdditiveScene(string currentScene, string targetScene) 
    {
        if (SM.GetActiveScene().buildIndex != 0) // If the active scene isnt the root (in editor mode for example)
        {
            SM.LoadScene(targetScene);
        }
        else
        {
            SM.UnloadSceneAsync(currentScene);
            SM.LoadSceneAsync(targetScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }

    public static void OpenAdditiveScene()
    {

    }

    public static void CloseAdditiveScene()
    {

    }
}
