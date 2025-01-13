using UnityEngine;

#pragma warning disable CS0162

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
        // 壁の設定
        wall = GetWall();

        // 壁の解除状態の設定
        if (wall != null)
            SetWallUnlocked();
        else if (wall == null &&
            eventName != RunGameEventData.RunGameEventNameEnum.Quest1 &&
            eventName != RunGameEventData.RunGameEventNameEnum.Quest2 &&
            eventName != RunGameEventData.RunGameEventNameEnum.Quest3)
        {
            Debug.LogError("壁が設定されていません。：" + this.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 最初の壁を設定
    private GameObject GetWall()
    {
        // Homeの壁
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放)
            return GameObject.Find("Wall_ToA");
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放)
            return GameObject.Find("Wall_ToB");
        // 難易度の壁
        else
        {
            // Tag,DifficultyLevelAreaを全て取得
            GameObject[] difficultyAreas = GameObject.FindGameObjectsWithTag("DifficultyLevelArea");
            // DifficultyLevelAreaのScript,設定されているdifficultyLevelによって取得する壁を変更
            foreach (GameObject difficultyArea in difficultyAreas)
            {
                switch (eventName)
                {
                    case RunGameEventData.RunGameEventNameEnum.HomeC中級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_中級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    case RunGameEventData.RunGameEventNameEnum.HomeC上級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_上級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    case RunGameEventData.RunGameEventNameEnum.HomeA中級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_中級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    case RunGameEventData.RunGameEventNameEnum.HomeA上級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_上級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    case RunGameEventData.RunGameEventNameEnum.HomeB中級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_中級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    case RunGameEventData.RunGameEventNameEnum.HomeB上級解放:
                        if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_上級)
                            return difficultyArea.transform.Find("Lock_Collider").gameObject;
                        break;

                    default:
                        return null;
                        break;
                }
            }

            return null;
        }
    }

    // 壁の解除状態の設定
    private void SetWallUnlocked()
    {
        // Home
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA解放)
        {
            if (RunGameManager.instance.A_Home)
                wall.SetActive(false);
            if (RunGameManager.instance.B_Home)
                GameObject.Find("Wall_ToB").SetActive(false);
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB解放)
        {
            if (RunGameManager.instance.B_Home)
                wall.SetActive(false);
            //if (RunGameManager.instance.A_Home)
            //    GameObject.Find("Wall_ToA").SetActive(false);
        }


        // 難易度
        else if (RunGameManager.instance.A_intermediate)
            wall.SetActive(false);
        else if (RunGameManager.instance.A_Advanced)
            wall.SetActive(false);
        else if (RunGameManager.instance.B_intermediate)
            wall.SetActive(false);
        else if (RunGameManager.instance.B_Advanced)
            wall.SetActive(false);
        else if (RunGameManager.instance.C_intermediate)
            wall.SetActive(false);
        else if (RunGameManager.instance.C_Advanced)
            wall.SetActive(false);
    }

    // 条件を満たしている場合、壁を解除→会話の変更
    public void UnlockedWall()
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
    public void UseItemAndGetCoin()
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
}
