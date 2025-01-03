using UnityEngine;

public class RunGameNPCArea : MonoBehaviour
{
    [Header("イベント名")]
    public RunGameEventData.RunGameEventNameEnum eventName;    // イベント名

    [Header("Home、Level解除に必要なコインorクエストクリアで貰えるコイン")]
    public int Coin = 3000;

    [Header("NPCが欲しいもの(Questの場合に設定)")]
    public int item1;
    public int item2;
    public int item3;

    //[Header("Homeに行く壁")]
    private GameObject wall;

    // クエストクリア済みか
    private bool isQuestClear = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Home解放の場合
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放)
        {
            wall = GameObject.Find("Wall_ToA");
            // HomeAが解除されている場合
            if (wall != null && RunGameManager.instance.A_Home)
                wall.SetActive(false);            
            else
                Debug.Log("Wall_ToAが見つからない、またはHomeAが解除されていません");
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放)
        {
            wall = GameObject.Find("Wall_ToB");
            // HomeBが解除されている場合
            if (wall != null && RunGameManager.instance.B_Home)
                wall.SetActive(false);
            else
                Debug.Log("Wall_ToBが見つからない、またはHomeBが解除されていません");
        }
        // 難易度の壁設定
        // HomeC中級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC中級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeC中級が解除されている場合
            if (wall != null && RunGameManager.instance.C_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeC中級が解除されていません");
        }
        // HomeC上級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC上級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeC上級が解除されている場合
            if (wall != null && RunGameManager.instance.C_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advancedが見つからない、またはHomeC上級が解除されていません");
        }
        // HomeA中級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA中級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeA中級が解除されている場合
            if (wall != null && RunGameManager.instance.A_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeA中級が解除されていません");
        }
        // HomeA上級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA上級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeA上級が解除されている場合
            if (wall != null && RunGameManager.instance.A_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advancedが見つからない、またはHomeA上級が解除されていません");
        }
        // HomeB中級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB中級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeB中級が解除されている場合
            if (wall != null && RunGameManager.instance.B_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeB中級が解除されていません");
        }
        // HomeB上級解放の場合
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB上級解放)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeB上級が解除されている場合
            if (wall != null && RunGameManager.instance.B_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advancedが見つからない、またはHomeB上級が解除されていません");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 条件を満たしている場合、壁を解除
    public void SetUnlockedWall()
    {
        // Enumが壁の解放の場合
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放 || eventName == RunGameEventData.RunGameEventNameEnum.HomeA中級解放 || eventName == RunGameEventData.RunGameEventNameEnum.HomeA上級解放 ||
            eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放 || eventName == RunGameEventData.RunGameEventNameEnum.HomeB中級解放 || eventName == RunGameEventData.RunGameEventNameEnum.HomeB上級解放 ||
            eventName == RunGameEventData.RunGameEventNameEnum.HomeC中級解放 || eventName == RunGameEventData.RunGameEventNameEnum.HomeC上級解放)
        {

            // 初回の会話は再生する
            if (NPCDialogueManager.instance.GetFirstTalk_RunGame(eventName))
                return;
            else if (RunGameManager.instance.coin >= Coin && !GetUnlockedWall())
            {
                NPCDialogueManager.instance.SetCheckPoint_RunGame(eventName);
                RunGameManager.instance.coin -= Coin;
                wall.SetActive(false);
                SetUnlockedWall(true);
            }
        }
    }

    // 条件を満たしている場合、Coinを獲得＆itemを消費
    public void GetCoinAndUseItem()
    {
        // Questの場合
        if (eventName == RunGameEventData.RunGameEventNameEnum.Quest1 || eventName == RunGameEventData.RunGameEventNameEnum.Quest2 || eventName == RunGameEventData.RunGameEventNameEnum.Quest3)
        {
            // 初回の会話は再生する＆itemが全て0の場合もReturn
            if (NPCDialogueManager.instance.GetFirstTalk_RunGame(eventName) || (item1 == 0 && item2 == 0 && item3 == 0))
                return;
            else if (RunGameManager.instance.item1 >= item1 && RunGameManager.instance.item2 >= item2 && RunGameManager.instance.item3 >= item3 && !isQuestClear)
            {
                NPCDialogueManager.instance.SetCheckPoint_RunGame(eventName);
                RunGameManager.instance.coin += Coin;
                RunGameManager.instance.item1 -= item1;
                RunGameManager.instance.item2 -= item2;
                RunGameManager.instance.item3 -= item3;

                isQuestClear = true;
            }
        }
    }

    // 壁の解除状態を保存
    private void SetUnlockedWall(bool isUnlocked)
    {
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA上級解放)
        {
            RunGameManager.instance.A_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA中級解放)
        {
            RunGameManager.instance.A_intermediate = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放)
        {
            RunGameManager.instance.A_Home = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB上級解放)
        {
            RunGameManager.instance.B_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB中級解放)
        {
            RunGameManager.instance.B_intermediate = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放)
        {
            RunGameManager.instance.B_Home = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC上級解放)
        {
            RunGameManager.instance.C_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC中級解放)
        {
            RunGameManager.instance.C_intermediate = isUnlocked;
        }
    }

    // 壁の解除状態を取得
    private bool GetUnlockedWall()
    {
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA上級解放)
        {
            return RunGameManager.instance.A_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA中級解放)
        {
            return RunGameManager.instance.A_intermediate;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放)
        {
            return RunGameManager.instance.A_Home;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB上級解放)
        {
            return RunGameManager.instance.B_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB中級解放)
        {
            return RunGameManager.instance.B_intermediate;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放)
        {
            return RunGameManager.instance.B_Home;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC上級解放)
        {
            return RunGameManager.instance.C_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC中級解放)
        {
            return RunGameManager.instance.C_intermediate;
        }
        return false;
    }

    // 難易度の壁を取得
    private GameObject GetDifficultyWall()
    {
        // Tag,DifficultyLevelAreaを全て取得
        GameObject[] difficultyAreas = GameObject.FindGameObjectsWithTag("DifficultyLevelArea");
        // DifficultyLevelAreaのScript,設定されているdifficultyLevelによって取得する壁を変更
        foreach (GameObject difficultyArea in difficultyAreas)
        {
            if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeC中級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_中級)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeC上級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_上級)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeA中級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_中級)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeA上級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_上級)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeB中級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_中級)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeB上級解放)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_上級)
                    return difficultyArea;
            }
        }

        return null;
    }
}
