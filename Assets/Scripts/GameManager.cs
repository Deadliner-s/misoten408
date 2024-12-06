using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerのインスタンス
    public static GameManager instance;

    // 制限時間
    public float timeLimit = 180f;

    // 時間
    public float time = 0f;

    // ゲーム中かどうか
    private bool onGame = false;

    private void Awake()
    {
        // インスタンスが存在しない場合
        if (instance == null)
        {
            instance = this;                // このインスタンスを代入
            DontDestroyOnLoad(gameObject);  // シーン遷移時に破棄されないように設定
        }
        // インスタンスが存在する場合
        else
        {
            Destroy(gameObject); // このインスタンスを破棄
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // FPS固定
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム中の場合
        if (onGame)
        {
            // 時間を減らす
            time -= Time.deltaTime;

            // 時間が0以下の場合
            if (time <= 0)
            {
                time = 0;
                SceneTransitionManager.instance.LoadSceneAsync("Result");
                onGame = false;
            }
        }
    }

    // ゲーム開始
    public void StartGame()
    {
        onGame = true;
        time = timeLimit;
    }

    // ゲーム終了
    public void EndGame()
    {
        onGame = false;
    }

    // ゲーム中かどうかを返す
    public bool IsOnGame()
    {
        return onGame;
    }

    public void SetTimer(float t)
    {
        timeLimit = t;
    }

    
}

