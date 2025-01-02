//using UnityEngine;

//public class UnlockedHome : MonoBehaviour
//{
//    [Header("イベント名")]
//    public RunEventData.RunEventNameEnum eventName;    // イベント名

//    [Header("解除するHome")]
//    public RunGameManager.HomeType homeType;

//    [Header("Home解除に必要なコイン")]
//    public int unlockCoin = 5000;

//    //[Header("Homeに行く壁")]
//    private GameObject wall;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        // 壁の設定
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//            wall = GameObject.Find("Wall_ToA");
//        }
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//            wall = GameObject.Find("Wall_ToB");
//        }
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//            wall = GameObject.Find("Wall_ToC");
//        }

//        // HomeAの場合
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//            // HomeAが解除されている場合
//            if (RunGameManager.instance.A_Home)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//        // HomeBの場合
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//            // HomeBが解除されている場合
//            if (RunGameManager.instance.B_Home)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//        // HomeCの場合
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//            // HomeCが解除されている場合
//            if (RunGameManager.instance.C_intermediate)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // HomeAの場合
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//            // HomeAが解除されている場合
//            if (RunGameManager.instance.A_Home)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//        // HomeBの場合
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//            // HomeBが解除されている場合
//            if (RunGameManager.instance.B_Home)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//        // HomeCの場合
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//            // HomeCが解除されている場合
//            if (RunGameManager.instance.C_intermediate)
//            {
//                // 壁を非表示にする
//                wall.SetActive(false);
//            }
//        }
//    }
//}
