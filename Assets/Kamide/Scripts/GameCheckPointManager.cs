using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    Vector3 StartDeckPos;
    Vector3 EndHandPos;
    bool isFinished;
    bool isDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // このオブジェクトを破壊しないようにする
        DontDestroyOnLoad(this);

        // フラグ初期化
        isFinished = false;
        isDestroy = false;   
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
        if (isFinished == true && isDestroy == true)
        {
            if(checkpointUI != null)
            {
               

                // UIの削除
                Destroy(checkpointUI);
                isDestroy = false;
                isFinished = false;
            }
        }
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
        if (stage == currentStageNum && isWorked == true)
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
            checkpointUI.transform.Find("backGround2/Description").GetComponent<TextMeshProUGUI>().text = description;
            checkpointUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = name;
       
            // UI移動処理
            StartDeckPos = checkpointUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-530, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());

            // フラグの切り替え
            isFinished = false;
        }       
    }

    /// <summary>
    /// チェックポイントUIの削除
    /// </summary>
    public void DestroyUI()
    {
        if (checkpointUI != null && isFinished)
        {      
            // フラグの切り替え
            isDestroy = true;
            isFinished = false;

            // UI移動処理
            StartDeckPos = checkpointUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-1400, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());           
        }
    }

    private IEnumerator MoveUI()
    {       
        float animDuration = 0.5f; // アニメーションの総時間
        float startTime = Time.time;
        while (Time.time - startTime < animDuration)
        {
            float journeyFraction = (Time.time - startTime) / animDuration;
            journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
            checkpointUI.transform.GetChild(0).localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
            yield return null;
        }

        isFinished = true;       
    }

}
