using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunGameResult : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject resultCanvas; // リザルト用キャンバス
    [SerializeField] private TextMeshProUGUI coinText; // コイン数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item1Text; // アイテム1の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item2Text; // アイテム2の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item3Text; // アイテム3の数を表示するUIテキスト

    [SerializeField] private Button button;
    [SerializeField] private GameObject playerStartPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreSystem = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<ScoreSystem>();

        if (scoreSystem == null)
        {
            Debug.LogError("ScoreSystemが見つかりませんでした。シーンに追加してください。");
            return;
        }

        resultCanvas.GetComponentInChildren<Button>().onClick.AddListener(ReturnHome);
    }
    public void UpdateResultDisplay()
    {       
        // リザルトキャンバスをアクティブ化
        if (resultCanvas != null)
        {
            resultCanvas.SetActive(true);
        }
        // スコアシステムから値を取得してUIに表示
        coinText.text = "Coins: " + scoreSystem.currentkoin;
        item1Text.text = "Item 1: " + scoreSystem.currentitem1;
        item2Text.text = "Item 2: " + scoreSystem.currentitem2;
        item3Text.text = "Item 3: " + scoreSystem.currentitem3;
    }

    private void ReturnHome()
    {
        SceneTransitionManager.instance.LoadSceneAsyncPlayerSetpos("home", playerStartPos);
    }
}
