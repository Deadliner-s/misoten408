//using UnityEngine;

//public class UnlockedLevelNPC: MonoBehaviour
//{
//    [Header("�C�x���g��")]
//    public RunEventData.RunEventNameEnum eventName;    // �C�x���g��

//    [Header("���݂�Home")]
//    public RunGameManager.HomeType homeType;

//    [Header("��������Level")]
//    public RunGameManager.DifficultyLevel difficultyLevel;

//    [Header("Level�����ɕK�v�ȃR�C��")]
//    public int unlockCoin = 5000;

//    [Header(��Փx�̕�")]
//    private GameObject wall;

//     Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//         �ǂ̐ݒ�
//        if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//        {
//            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
//        }
//        else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//        {
//            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
//        }

//         HomeA�̏ꍇ
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//             Intermediate(����)�̏ꍇ
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.A_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(�㋉)�̏ꍇ
//            else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//            {
//                if (RunGameManager.instance.A_Advanced)
//                {
//                    wall.SetActive(false);
//                }
//            }
//        }
//         HomeB�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//             Intermediate(����)�̏ꍇ
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.B_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(�㋉)�̏ꍇ
//            else if (difficultyLevel == RunGameManager.DifficultyLevel.Advanced)
//            {
//                if (RunGameManager.instance.B_Advanced)
//                {
//                    wall.SetActive(false);
//                }
//            }
//        }
//         HomeC�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//             Intermediate(����)�̏ꍇ
//            if (difficultyLevel == RunGameManager.DifficultyLevel.Intermediate)
//            {
//                if (RunGameManager.instance.C_intermediate)
//                {
//                    wall.SetActive(false);
//                }
//            }
//             Advanced(�㋉)�̏ꍇ
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
//         �����𕥂������ɕǂ��A�N�e�B�u�ɂ��鏈��������
//    }
//}
