//using UnityEngine;

//public class UnlockedHome : MonoBehaviour
//{
//    [Header("�C�x���g��")]
//    public RunEventData.RunEventNameEnum eventName;    // �C�x���g��

//    [Header("��������Home")]
//    public RunGameManager.HomeType homeType;

//    [Header("Home�����ɕK�v�ȃR�C��")]
//    public int unlockCoin = 5000;

//    //[Header("Home�ɍs����")]
//    private GameObject wall;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        // �ǂ̐ݒ�
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

//        // HomeA�̏ꍇ
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//            // HomeA����������Ă���ꍇ
//            if (RunGameManager.instance.A_Home)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//        // HomeB�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//            // HomeB����������Ă���ꍇ
//            if (RunGameManager.instance.B_Home)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//        // HomeC�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//            // HomeC����������Ă���ꍇ
//            if (RunGameManager.instance.C_intermediate)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // HomeA�̏ꍇ
//        if (homeType == RunGameManager.HomeType.HomeA)
//        {
//            // HomeA����������Ă���ꍇ
//            if (RunGameManager.instance.A_Home)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//        // HomeB�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeB)
//        {
//            // HomeB����������Ă���ꍇ
//            if (RunGameManager.instance.B_Home)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//        // HomeC�̏ꍇ
//        else if (homeType == RunGameManager.HomeType.HomeC)
//        {
//            // HomeC����������Ă���ꍇ
//            if (RunGameManager.instance.C_intermediate)
//            {
//                // �ǂ��\���ɂ���
//                wall.SetActive(false);
//            }
//        }
//    }
//}
