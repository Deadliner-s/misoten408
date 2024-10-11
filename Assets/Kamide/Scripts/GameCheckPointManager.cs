using UnityEngine;

public class GameCheckPointManager : MonoBehaviour
{
    // �ϐ��錾
    [Header("���݂̃X�e�[�W�ԍ�")]
    public int currentStageNum = 0;
    public StageCheckPointManager[] stageCheckPointManagers;
    bool canCreateAll = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �ŏ��̃X�e�[�W�̃`�F�b�N�|�C���g����
        stageCheckPointManagers[currentStageNum].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();

        // ���̃I�u�W�F�N�g��j�󂵂Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (canCreateAll)
        {
            stageCheckPointManagers[currentStageNum].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();
            canCreateAll = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �t���O�؂�ւ��p�֐�
    /// </summary>
    /// <param name="stage">�X�e�[�W�ԍ�</param>
    /// <param name="cp_num">�`�F�b�N�|�C���g�t��</param>
    /// <param name="isWorked">�t���O�̒l</param>
    public void ChangeFlag(int stage, int cp_num, bool isWorked)
    {
        stageCheckPointManagers[stage].GetComponent<StageCheckPointManager>().checkPoints[cp_num].cp_isWorked = isWorked;

        if (stage == currentStageNum &&  isWorked == true)
        {
            stageCheckPointManagers[stage].GetComponent<StageCheckPointManager>().CreateCheckPoint(cp_num);
        }
    }

    /// <summary>
    /// �X�e�[�W�`�F���W���ɌĂяo���֐�
    /// </summary>
    /// <param name="nextStage">���̃X�e�[�W�ԍ�</param>
    public void ChangeStage(int nextStage)
    {       
        // ���̃X�e�[�W�ԍ���ۑ�
        currentStageNum = nextStage;

        // �t���O�̐؂�ւ�
        canCreateAll = true;
    }
}
