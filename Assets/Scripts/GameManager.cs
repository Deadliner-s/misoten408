using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerのインスタンス
    public static GameManager instance;

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
        
    }
}
