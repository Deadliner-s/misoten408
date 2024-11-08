using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public GameObject loadingScreen;    // ロード画面のUI（プレハブなどをアタッチ）
    public Slider progressBar;          // ロード中の進捗を表示するスライダー   
    //[SerializeField]
    public GameObject fadePanel;        // フェードに使うパネル
    // [SerializeField]
    FadeManager fadeManager;

    [SerializeField]
    private Vector3 playerStartPos = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    private GameObject PlayerStartObj;
    private GameCheckPointManager gameCheckPointManager;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (instance == null)
        {
            if (!FadeManager.isFadeInstance)
            {
                Instantiate(fadePanel);
                fadeManager = GameObject.FindWithTag("Fade").GetComponent<FadeManager>();

            }

            instance = this;
            DontDestroyOnLoad(gameObject);  // シーン間でオブジェクトが破棄されないようにする

            playerStartPos = PlayerStartObj.transform.position;

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

        // フェードアウト開始
        fadeManager.FadeOut();

        // シーンを非同期でロード
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        yield return new WaitForSeconds(2);

        // ロード中の進捗を更新
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;  // 進捗バーの値を更新
                                           // フェードイン開始
            fadeManager.FadeIn();

            operation.allowSceneActivation = true;  // シーン表示を許可

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
