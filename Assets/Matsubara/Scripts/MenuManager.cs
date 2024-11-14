using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private GameObject menuUI; // メニューUIパネルをアタッチする
    private GameObject menuUIInstance;

    private void Awake()
    {
        // シングルトンの設定：このオブジェクトが唯一のインスタンスになるようにする
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 複数のインスタンスを防ぐために破棄
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを保持する
            
            // menuUIが存在し、シーン切り替え時も保持したい場合
            if (menuUI != null && menuUIInstance == null)
            {
                menuUIInstance = Instantiate(menuUI);
                DontDestroyOnLoad(menuUIInstance); // menuUIがMenuManagerの子でない場合に保持
                menuUIInstance.SetActive(false);    // 最初は非表示
            }
        }

    }

    private void Update()
    {
        // Escapeキーが押されたらメニューの表示/非表示を切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // メニューの表示を切り替えるメソッド
    public void ToggleMenu()
    {
        if(!menuUIInstance.activeSelf)
        {
            // 表示
            ShowMenu();

        }
        else
        {
            // 非表示
            HideMenu();

        }

    }

    // メニューを表示するメソッド
    public void ShowMenu()
    {
        if (menuUIInstance != null)
        {
            menuUIInstance.SetActive(true);
            Time.timeScale = 0f;    // ポーズ
        }
    }

    // メニューを非表示にするメソッド
    public void HideMenu()
    {
        if (menuUIInstance != null)
        {
            menuUIInstance.SetActive(false);
            Time.timeScale = 1f;   // 再開
        }
    }
}
