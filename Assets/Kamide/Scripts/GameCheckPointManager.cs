using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameCheckPointManager : MonoBehaviour
{
    // 変数宣言
    [Header("現在のステージ番号")]
    public int currentStageNum = 0;
    public StageCheckPointManager[] stageCheckPointManagers;
    [Header("UIPrefab")]
    public GameObject cp_UI;
    GameObject checkpointUI;
    bool canCreateAll = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 最初のステージのチェックポイント生成
        stageCheckPointManagers[currentStageNum].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();

        // このオブジェクトを破壊しないようにする
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (canCreateAll)
        {
            stageCheckPointManagers[currentStageNum].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();
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
        stageCheckPointManagers[stage].GetComponent<StageCheckPointManager>().checkPoints[cp_num].cp_isWorked = isWorked;

        if (stage == currentStageNum &&  isWorked == true)
        {
            stageCheckPointManagers[stage].GetComponent<StageCheckPointManager>().CreateCheckPoint(cp_num);
        }
    }

    /// <summary>
    /// ステージチェンジ時に呼び出す関数
    /// </summary>
    /// <param name="nextStage">次のステージ番号</param>
    public void ChangeStage(int nextStage)
    {       
        // 次のステージ番号を保存
        currentStageNum = nextStage;

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
        string description = stageCheckPointManagers[stageNum].checkPoints[indexNum].cp_description;
        string name = stageCheckPointManagers[stageNum].checkPoints[indexNum].cp_name;

        // UIの生成
        checkpointUI = Instantiate(cp_UI);
        checkpointUI.transform.Find("backGround1/Description").GetComponent<TextMeshProUGUI>().text = description;
        checkpointUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = name;
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
        }
    }
}
