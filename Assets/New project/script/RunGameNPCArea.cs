using UnityEngine;

public class RunGameNPCArea : MonoBehaviour
{
    [Header("�C�x���g��")]
    public RunGameEventData.RunGameEventNameEnum eventName;    // �C�x���g��

    [Header("Home�ALevel�����ɕK�v�ȃR�C��or�N�G�X�g�N���A�ŖႦ��R�C��")]
    public int Coin = 3000;

    [Header("NPC���~��������(Quest�̏ꍇ�ɐݒ�)")]
    public int item1;
    public int item2;
    public int item3;

    //[Header("Home�ɍs����")]
    private GameObject wall;

    // �N�G�X�g�N���A�ς݂�
    private bool isQuestClear = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Home����̏ꍇ
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA���)
        {
            wall = GameObject.Find("Wall_ToA");
            // HomeA����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_Home)
                wall.SetActive(false);            
            else
                Debug.Log("Wall_ToA��������Ȃ��A�܂���HomeA����������Ă��܂���");
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB���)
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
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�������)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeC��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.C_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeC��������������Ă��܂���");
        }
        // HomeC�㋉����̏ꍇ
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�㋉���)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeC�㋉����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.C_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advanced��������Ȃ��A�܂���HomeC�㋉����������Ă��܂���");
        }
        // HomeA��������̏ꍇ
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�������)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeA��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeA��������������Ă��܂���");
        }
        // HomeA�㋉����̏ꍇ
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�㋉���)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeA�㋉����������Ă���ꍇ
            if (wall != null && RunGameManager.instance.A_Advanced)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Advanced��������Ȃ��A�܂���HomeA�㋉����������Ă��܂���");
        }
        // HomeB��������̏ꍇ
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�������)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
            // HomeB��������������Ă���ꍇ
            if (wall != null && RunGameManager.instance.B_intermediate)
                wall.SetActive(false);
            else
                Debug.Log("Lock_Intermediate��������Ȃ��A�܂���HomeB��������������Ă��܂���");
        }
        // HomeB�㋉����̏ꍇ
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�㋉���)
        {
            wall = GetDifficultyWall().transform.Find("Lock_Collider").gameObject;
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
    }

    // �����𖞂����Ă���ꍇ�A�ǂ�����
    public void SetUnlockedWall()
    {
        // Enum���ǂ̉���̏ꍇ
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA��� || eventName == RunGameEventData.RunGameEventNameEnum.HomeA������� || eventName == RunGameEventData.RunGameEventNameEnum.HomeA�㋉��� ||
            eventName == RunGameEventData.RunGameEventNameEnum.HomeB��� || eventName == RunGameEventData.RunGameEventNameEnum.HomeB������� || eventName == RunGameEventData.RunGameEventNameEnum.HomeB�㋉��� ||
            eventName == RunGameEventData.RunGameEventNameEnum.HomeC������� || eventName == RunGameEventData.RunGameEventNameEnum.HomeC�㋉���)
        {

            // ����̉�b�͍Đ�����
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

    // �����𖞂����Ă���ꍇ�ACoin���l����item������
    public void GetCoinAndUseItem()
    {
        // Quest�̏ꍇ
        if (eventName == RunGameEventData.RunGameEventNameEnum.Quest1 || eventName == RunGameEventData.RunGameEventNameEnum.Quest2 || eventName == RunGameEventData.RunGameEventNameEnum.Quest3)
        {
            // ����̉�b�͍Đ����違item���S��0�̏ꍇ��Return
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

    // �ǂ̉�����Ԃ�ۑ�
    private void SetUnlockedWall(bool isUnlocked)
    {
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�㋉���)
        {
            RunGameManager.instance.A_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�������)
        {
            RunGameManager.instance.A_intermediate = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA���)
        {
            RunGameManager.instance.A_Home = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�㋉���)
        {
            RunGameManager.instance.B_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�������)
        {
            RunGameManager.instance.B_intermediate = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB���)
        {
            RunGameManager.instance.B_Home = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�㋉���)
        {
            RunGameManager.instance.C_Advanced = isUnlocked;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�������)
        {
            RunGameManager.instance.C_intermediate = isUnlocked;
        }
    }

    // �ǂ̉�����Ԃ��擾
    private bool GetUnlockedWall()
    {
        if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�㋉���)
        {
            return RunGameManager.instance.A_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA�������)
        {
            return RunGameManager.instance.A_intermediate;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeA���)
        {
            return RunGameManager.instance.A_Home;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�㋉���)
        {
            return RunGameManager.instance.B_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB�������)
        {
            return RunGameManager.instance.B_intermediate;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeB���)
        {
            return RunGameManager.instance.B_Home;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�㋉���)
        {
            return RunGameManager.instance.C_Advanced;
        }
        else if (eventName == RunGameEventData.RunGameEventNameEnum.HomeC�������)
        {
            return RunGameManager.instance.C_intermediate;
        }
        return false;
    }

    // ��Փx�̕ǂ��擾
    private GameObject GetDifficultyWall()
    {
        // Tag,DifficultyLevelArea��S�Ď擾
        GameObject[] difficultyAreas = GameObject.FindGameObjectsWithTag("DifficultyLevelArea");
        // DifficultyLevelArea��Script,�ݒ肳��Ă���difficultyLevel�ɂ���Ď擾����ǂ�ύX
        foreach (GameObject difficultyArea in difficultyAreas)
        {
            if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeC�������)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_����)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeC�㋉���)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_�㋉)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeA�������)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_����)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeA�㋉���)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_�㋉)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeB�������)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_����)
                    return difficultyArea;
            }
            else if (this.eventName == RunGameEventData.RunGameEventNameEnum.HomeB�㋉���)
            {
                if (difficultyArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_�㋉)
                    return difficultyArea;
            }
        }

        return null;
    }
}
