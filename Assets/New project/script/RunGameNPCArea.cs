using UnityEngine;

public class RunGameNPCArea : MonoBehaviour
{
    [Header("イベント名")]
    public RunEventData.RunEventNameEnum eventName;    // イベント名

    [Header("Home解除に必要なコインorクエストクリアで貰えるコイン")]
    public int unlockCoin = 5000;

    //[Header("Homeに行く壁")]
    private GameObject wall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Home解放の場合
        if (eventName == RunEventData.RunEventNameEnum.HomeA解放)
        {
            wall = GameObject.Find("Wall_ToA");
            // HomeAが解除されている場合
            if (wall != null && RunGameManager.instance.A_Home)
                wall.SetActive(false);            
            else
                Debug.Log("Wall_ToAが見つからない、またはHomeAが解除されていません");
        }
        else if (eventName == RunEventData.RunEventNameEnum.HomeB解放)
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
        else if (eventName == RunEventData.RunEventNameEnum.HomeC中級解放)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeC中級が解除されている場合
            if (wall != null && RunGameManager.instance.C_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeC中級が解除されていません");
        }
        // HomeC上級解放の場合
        else if (eventName == RunEventData.RunEventNameEnum.HomeC上級解放)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
            // HomeC上級が解除されている場合
            if (wall != null && RunGameManager.instance.C_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advancedが見つからない、またはHomeC上級が解除されていません");
        }
        // HomeA中級解放の場合
        else if (eventName == RunEventData.RunEventNameEnum.HomeA中級解放)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeA中級が解除されている場合
            if (wall != null && RunGameManager.instance.A_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeA中級が解除されていません");
        }
        // HomeA上級解放の場合
        else if (eventName == RunEventData.RunEventNameEnum.HomeA上級解放)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
            // HomeA上級が解除されている場合
            if (wall != null && RunGameManager.instance.A_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advancedが見つからない、またはHomeA上級が解除されていません");
        }
        // HomeB中級解放の場合
        else if (eventName == RunEventData.RunEventNameEnum.HomeB中級解放)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeB中級が解除されている場合
            if (wall != null && RunGameManager.instance.B_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediateが見つからない、またはHomeB中級が解除されていません");
        }
        // HomeB上級解放の場合
        else if (eventName == RunEventData.RunEventNameEnum.HomeB上級解放)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
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
        // RunGameNPCAreaから取得した情報によってcheckPointを増やし、会話の内容を変えるため
        // NPCDialogueManager.instance.SetCheckPoint_RunGame(eventName);を呼び出す
    }
}
