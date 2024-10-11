using UnityEngine;

public class MenuController : MonoBehaviour
{
    // メニュー画面のパネル（UI）
    public GameObject menuPanel;

    // ゲームがポーズ中かどうかを管理するフラグ
    private bool isPaused = false;

    void Start()
    {
        // メニューを非表示にしておく
        menuPanel.SetActive(false);
    }

    void Update()
    {
        // Escapeキーが押されたらメニューの表示/非表示を切り替える
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // メニューが表示されている場合はゲームに戻る
            }
            else
            {
                PauseGame(); // メニューが表示されていない場合はメニューを開く
            }
        }
    }

    // ゲームをポーズしてメニューを表示する
    void PauseGame()
    {
        menuPanel.SetActive(true); // メニューを表示
        Time.timeScale = 0f; // 時間を止める（ゲームをポーズ）
        isPaused = true;
    }

    // メニューを閉じてゲームに戻る
    void ResumeGame()
    {
        menuPanel.SetActive(false); // メニューを非表示
        Time.timeScale = 1f; // 時間を再開する（ゲームを再開）
        isPaused = false;
    }
}
