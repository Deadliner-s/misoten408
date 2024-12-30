using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int currentScore = 0;      // 現在のスコア
    public int currentkoin = 0;         // コイン獲得総数
    public int currentitem1 = 0;        // 固定値加算アイテム
    public int currentitem2 = 0;        // 所持コインを1.3倍にする倍率加算アイテム
    public int currentitem3 = 0;        // 獲得したコイン枚数に応じて加算するアイテム
    public Text scoreText;               // スコアを表示するUI

    private void Start()
    {
        UpdateScoreUI(); // 初期スコアを表示
    }

    void OnTriggerEnter(Collider other)
    {
        // スコアアイテムタグのオブジェクトに触れた場合
        if (other.CompareTag("ScoreItem"))
        {
            currentScore += 10;  // スコアを加算
            currentkoin += 1;   //コインカウント
            UpdateScoreUI();    // UIを更新
            Destroy(other.gameObject); // アイテムを削除
        }
        // 障害物タグのオブジェクトに触れた場合
        if (other.CompareTag("syougaibutu"))
        {
            currentScore -= 50; // スコアを減算
            UpdateScoreUI();    // UIを更新

        }
        // item1タグのオブジェクトに触れた場合
        if (other.CompareTag("item1"))
        {
            currentScore += 200; // スコアを加算（100）
            UpdateScoreUI();    // UIを更新
            Destroy(other.gameObject); // アイテムを削除

        }
        // item2タグのオブジェクトに触れた場合
        if (other.CompareTag("item2"))
        {
            currentScore = currentScore / 10 * 12; ; // スコアを1.2倍にする
            UpdateScoreUI();    // UIを更新
            Destroy(other.gameObject); // アイテムを削除

        }
        // item3タグのオブジェクトに触れた場合
        if (other.CompareTag("item3"))
        {
            currentScore = currentScore + currentkoin * 2; // コイン１枚ごとに2増加
            UpdateScoreUI();    // UIを更新
            Destroy(other.gameObject); // アイテムを削除

        }

    }
    // スコアUIを更新するメソッド
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }
    
}

