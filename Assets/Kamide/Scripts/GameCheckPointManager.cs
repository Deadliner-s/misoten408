using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameCheckPointManager : MonoBehaviour
{
    // 変数宣言
    [Header("現在のステージ番号")]
    public int currentStageNum = 0; // ステージ:A～E=1～5 その他のシーン:0
    public StageCheckPointManager[] stageCheckPointManagers;
    [Header("UIPrefab")]
    public GameObject cp_UI;
    GameObject checkpointUI;        // チェックポイントUI格納用
    bool canCreateAll = false;      // チェックポイント生成フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        // このオブジェクトを破壊しないようにする
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (canCreateAll)
        {            
            // 配列範囲判定
            if (currentStageNum > 0 && currentStageNum <= stageCheckPointManagers.Length + 1)
            {                
                // チェックポイントの生成
                stageCheckPointManagers[currentStageNum - 1].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();
            }
            // フラグの切り替え
            canCreateAll = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// フラグ切り替え用関数
    /// </summary>
    /// <param name="stage">ステージ番号</param>
    /// <param name="cp_num">チェックポイント葉豪</param>
    /// <param name="isWorked">フラグの値</param>
    public void ChangeFlag(int stage, int cp_num, bool isWorked)
    {
        // フラグの切り替え
        stageCheckPointManagers[stage - 1].GetComponent<StageCheckPointManager>().checkPoints[cp_num].cp_isWorked = isWorked;

        // 現在のステージにチェックポイントがある場合
        if (stage == currentStageNum &&  isWorked == true)
        {
            stageCheckPointManagers[stage - 1].GetComponent<StageCheckPointManager>().CreateCheckPoint(cp_num);
        }
    }

    /// <summary>
    /// ステージチェンジ時に呼び出す関数
    /// </summary>
    /// <param name="nextStage">次のステージ番号</param>
    public void ChangeStage(string nextStage)
    {
        int stage = 0;

        switch (nextStage)
        {
            case "Area_A": stage = 1; break;
            case "Area_B": stage = 2; break;
            case "Area_C": stage = 3; break;
            case "Area_D": stage = 4; break;
            case "Area_E": stage = 5; break;
            default: stage = 0; break;
        }

        // 次のステージ番号を保存
        currentStageNum = stage;

        // フラグの切り替え
        canCreateAll = true;        
    }

    /// <summary>
    /// チェックポイントUIの生成
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    /// <param name="indexNum">配列添え字</param>
    public void CreateUI(int stageNum, int indexNum)
    {
        // UI作成に必要な情報の取得
        string description = stageCheckPointManagers[stageNum - 1].checkPoints[indexNum - 1].cp_description;
        string name = stageCheckPointManagers[stageNum - 1].checkPoints[indexNum - 1].cp_name;

        // UIの生成
        if (checkpointUI == null)
        {
            checkpointUI = Instantiate(cp_UI);
            checkpointUI.transform.Find("backGround1/Description").GetComponent<TextMeshProUGUI>().text = description;
            checkpointUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = name;
        }
    }

    /// <summary>
    /// チェックポイントUIの削除
    /// </summary>
    public void DestroyUI()
    {      
        if (checkpointUI != null)
        {
            // 削除処理
            Destroy(checkpointUI);
            checkpointUI = null;
        }
    }
}
