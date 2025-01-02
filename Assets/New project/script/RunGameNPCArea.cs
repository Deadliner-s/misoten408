using UnityEngine;

public class RunGameNPCArea : MonoBehaviour
{
    [Header("�C�x���g��")]
    public RunEventData.RunEventNameEnum eventName;    // �C�x���g��

    [Header("Home�����ɕK�v�ȃR�C��or�N�G�X�g�N���A�ŖႦ��R�C��")]
    public int unlockCoin = 5000;

    //[Header("Home�ɍs����")]
    private GameObject wall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Home����̏ꍇ
        if (eventName == RunEventData.RunEventNameEnum.HomeA���)
        {
            wall = GameObject.Find("Wall_ToA");
            // HomeA����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_Home)
                wall.SetActive(false);            
            else
                Debug.Log("Wall_ToA��������Ȃ��A�܂���HomeA����������Ă��܂���");
        }
        else if (eventName == RunEventData.RunEventNameEnum.HomeB���)
        {
            wall = GameObject.Find("Wall_ToB");
            // HomeB����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.B_Home)
                wall.SetActive(false);
            else
                Debug.Log("Wall_ToB��������Ȃ��A�܂���HomeB����������Ă��܂���");
        }
        // ��Փx�̕ǐݒ�
        // HomeC��������̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeC�������)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeC��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.C_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeC��������������Ă��܂���");
        }
        // HomeC�㋉����̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeC�㋉���)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
            // HomeC�㋉����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.C_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advanced��������Ȃ��A�܂���HomeC�㋉����������Ă��܂���");
        }
        // HomeA��������̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeA�������)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeA��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeA��������������Ă��܂���");
        }
        // HomeA�㋉����̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeA�㋉���)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
            // HomeA�㋉����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advanced��������Ȃ��A�܂���HomeA�㋉����������Ă��܂���");
        }
        // HomeB��������̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeB�������)
        {
            wall = GameObject.Find("IntermediateArea").transform.Find("Lock_Intermediate").gameObject;
            // HomeB��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.B_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeB��������������Ă��܂���");
        }
        // HomeB�㋉����̏ꍇ
        else if (eventName == RunEventData.RunEventNameEnum.HomeB�㋉���)
        {
            wall = GameObject.Find("AdvancedArea").transform.Find("Lock_Advanced").gameObject;
            // HomeB�㋉����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.B_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advanced��������Ȃ��A�܂���HomeB�㋉����������Ă��܂���");
        }


    }

    // Update is called once per frame
    void Update()
    {
        // RunGameNPCArea����擾�������ɂ����checkPoint�𑝂₵�A��b�̓��e��ς��邽��
        // NPCDialogueManager.instance.SetCheckPoint_RunGame(eventName);���Ăяo��
    }
}
