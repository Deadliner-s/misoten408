using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public GameObject loadingScreen;    // ロード画面のUI（プレハブなどをアタッチ）
    public Slider progressBar;          // ロード中の進捗を表示するスライダー   

    [SerializeField]
    private Vector3 playerStartPos = new Vector3(500.0f, 100.0f, 0.0f);
    private GameCheckPointManager gameCheckPointManager;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーン間でオブジェクトが破棄されないようにする

            // GameCheckPointManager取得
            gameCheckPointManager =  GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>();
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
    public void LoadSceneAsyncPlayerSetpos(string sceneName, GameObject SpawnPos)
    {
        playerStartPos = SpawnPos.transform.position;
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

        // 次のシーンを格納
        gameCheckPointManager.ChangeStage(sceneName);

        // ロードが完了したらロード画面を非表示
        loadingScreen.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            player.transform.position = playerStartPos;
        }
    }

}
