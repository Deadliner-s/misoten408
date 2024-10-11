using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public GameObject loadingScreen;    // ロード画面のUI（プレハブなどをアタッチ）
    public Slider progressBar;          // ロード中の進捗を表示するスライダー

    private void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーン間でオブジェクトが破棄されないようにする
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // シーンを即座に遷移させる（同期的に）
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // シーンを非同期にロードし、ロード画面を表示する
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        // ロード画面を表示
        loadingScreen.SetActive(true);

        // シーンを非同期でロード
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // ロード中の進捗を更新
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;  // 進捗バーの値を更新
            yield return null;
        }

        // ロードが完了したらロード画面を非表示
        loadingScreen.SetActive(false);
    }
}
