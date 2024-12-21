using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    SceneTransitionManager sceneManager;

    [SerializeField] private GameObject firstButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneTransitionManager.instance != null)
            sceneManager = SceneTransitionManager.instance;
        else
            Debug.LogWarning("SceneManager instance is null!");
        if(firstButton != null)
            // 最初に選択するボタンを設定
            EventSystem.current.SetSelectedGameObject(firstButton);

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

    // ゲームを終了する関数
    public void QuitGame()
    {
        // エディターで実行中の場合の終了処理
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後のアプリケーションを終了
        Application.Quit();
#endif
    }

    public void SelectButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
}
