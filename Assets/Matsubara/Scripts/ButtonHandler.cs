using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    SceneTransitionManager sceneManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneTransitionManager.instance != null)
            sceneManager = SceneTransitionManager.instance;
        else
            Debug.LogWarning("SceneManager instance is null!");

    }
    
    public void TitleOnButtonCllick()
    {
        if(sceneManager != null)
        {
            sceneManager.LoadSceneAsync("Area_C");
        }

    }

    public void ResultOnButtonCllick()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadSceneAsync("Title");
        }

    }

}
