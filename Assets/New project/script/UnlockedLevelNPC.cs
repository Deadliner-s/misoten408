//using UnityEngine;

//public class UnlockedLevelNPC: MonoBehaviour
//{
//    [Header("イベント名")]
//    public RunEventData.RunEventNameEnum eventName;    // イベント名

//    [Header("現在のHome")]
//    public RunGameManager.HomeType homeType;

//    [Header("解除するLevel")]
//    public RunGameManager.DifficultyLevel difficultyLevel;

//    [Header("Level解除に必要なコイン")]
//    public int unlockCoin = 5000;

//    [Header(難易度の壁")]
//    private GameObject wall;

//     Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//         壁の設定
//        if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//        {
//            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
//        }
//        else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//        {
//            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
//        }

//         HomeAの場合
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//             Intermediate(中級)の場合
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.A_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(上級)の場合
//            else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//            {
//                if (RunGameManager.instance.A_Advanced)
//                {
//                    wall.SetActive(false);
//                }
//            }
//        }
//         HomeBの場合
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//             Intermediate(中級)の場合
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.B_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(上級)の場合
//            else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//            {
//                if (RunGameManager.instance.B_Advanced)
//                {
//                    wall.SetActive(false);
//                }
//            }
//        }
//         HomeCの場合
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//             Intermediate(中級)の場合
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.C_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(上級)の場合
//            else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//            {
//                if (RunGameManager.instance.C_Advanced)
//                {
//                    wall.SetActive(false);
//                }
//            }
//        }
//    }

//     Update is called once per frame
//    void Update()
//    {
//         お金を払った時に壁を非アクティブにする処理を書く
//    }
//}
