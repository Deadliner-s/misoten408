using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject GamePanel; // ゲーム用キャンバス
    [SerializeField] private TextMeshProUGUI coinText; // コイン数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item1Text; // アイテム1の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item2Text; // アイテム2の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item3Text; // アイテム3の数を表示するUIテキスト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<ScoreSystem>();

        if (scoreSystem == null)
        {
            Debug.LogError("ScoreSystemが見つかりませんでした。シーンに追加してください。");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // スコアシステムから値を取得してUIに表示
        coinText.text = scoreSystem.currentScore.ToString();
        item1Text.text = scoreSystem.currentitem1.ToString();
        item2Text.text = scoreSystem.currentitem2.ToString();
        item3Text.text = scoreSystem.currentitem3.ToString();
    }

    public void HideCanvas()
    {
        GamePanel.SetActive(false);
    }
}
