using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RunGameResult : MonoBehaviour
{
    private ScoreSystem scoreSystem;

    [SerializeField] private GameObject resultCanvas; // リザルト用キャンバス
    [SerializeField] private TextMeshProUGUI coinText; // コイン数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item1Text; // アイテム1の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item2Text; // アイテム2の数を表示するUIテキスト
    [SerializeField] private TextMeshProUGUI item3Text; // アイテム3の数を表示するUIテキスト

    [SerializeField] private GameObject button;
    [SerializeField] private GameObject playerStartPos;
    [SerializeField] private string sceneName;

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
        coinText.text  = scoreSystem.currentScore.ToString();
        item1Text.text = scoreSystem.currentitem1.ToString();
        item2Text.text = scoreSystem.currentitem2.ToString();
        item3Text.text = scoreSystem.currentitem3.ToString();
    }

    private void ReturnHome()
    {
        SceneTransitionManager.instance.LoadSceneAsyncPlayerSetpos(sceneName, playerStartPos);
    }
}
