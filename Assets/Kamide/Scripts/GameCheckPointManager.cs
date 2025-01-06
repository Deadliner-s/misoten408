using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameCheckPointManager : MonoBehaviour
{
    // �ϐ��錾
    [Header("���݂̃X�e�[�W�ԍ�")]
    public int currentStageNum = 0; // �X�e�[�W:A�`E=1�`5 ���̑��̃V�[��:0
    public StageCheckPointManager[] stageCheckPointManagers;
    [Header("UIPrefab")]
    public GameObject cp_UI;
    GameObject checkpointUI;        // �`�F�b�N�|�C���gUI�i�[�p
    bool canCreateAll = false;      // �`�F�b�N�|�C���g�����t���O
    Vector3 StartDeckPos;
    Vector3 EndHandPos;
    bool isFinished;
    bool isDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���̃I�u�W�F�N�g��j�󂵂Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);

        // �t���O������
        isFinished = false;
        isDestroy = false;   
    }

    private void FixedUpdate()
    {
        if (canCreateAll)
        {
            // �z��͈͔���
            if (currentStageNum > 0 && currentStageNum <= stageCheckPointManagers.Length + 1)
            {
                // �`�F�b�N�|�C���g�̐���
                stageCheckPointManagers[currentStageNum - 1].GetComponent<StageCheckPointManager>().CreateAllCheckPoints();
            }
            // �t���O�̐؂�ւ�
            canCreateAll = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished == true && isDestroy == true)
        {
            if(checkpointUI != null)
            {
               

                // UI�̍폜
                Destroy(checkpointUI);
                isDestroy = false;
                isFinished = false;
            }
        }
    }

    /// <summary>
    /// �t���O�؂�ւ��p�֐�
    /// </summary>
    /// <param name="stage">�X�e�[�W�ԍ�</param>
    /// <param name="cp_num">�`�F�b�N�|�C���g�t��</param>
    /// <param name="isWorked">�t���O�̒l</param>
    public void ChangeFlag(int stage, int cp_num, bool isWorked)
    {
        // �t���O�̐؂�ւ�
        stageCheckPointManagers[stage - 1].GetComponent<StageCheckPointManager>().checkPoints[cp_num].cp_isWorked = isWorked;

        // ���݂̃X�e�[�W�Ƀ`�F�b�N�|�C���g������ꍇ
        if (stage == currentStageNum && isWorked == true)
        {
            stageCheckPointManagers[stage - 1].GetComponent<StageCheckPointManager>().CreateCheckPoint(cp_num);
        }
    }

    /// <summary>
    /// �X�e�[�W�`�F���W���ɌĂяo���֐�
    /// </summary>
    /// <param name="nextStage">���̃X�e�[�W�ԍ�</param>
    public void ChangeStage(string nextStage)
    {
        int stage = 0;

        switch (nextStage)
        {
            case "Area_A": stage = 1; break;
            case "Area_B": stage = 2; break;
            case "Area_C": stage = 3; break;
            case "Area_D": stage = 4; break;
            case "Area_E": stage = 5; break;
            default: stage = 0; break;
        }

        // ���̃X�e�[�W�ԍ���ۑ�
        currentStageNum = stage;

        // �t���O�̐؂�ւ�
        canCreateAll = true;
    }

    /// <summary>
    /// �`�F�b�N�|�C���gUI�̐���
    /// </summary>
    /// <param name="stageNum">�X�e�[�W�ԍ�</param>
    /// <param name="indexNum">�z��Y����</param>
    public void CreateUI(int stageNum, int indexNum)
    {
        // UI�쐬�ɕK�v�ȏ��̎擾
        string description = stageCheckPointManagers[stageNum - 1].checkPoints[indexNum - 1].cp_description;
        string name = stageCheckPointManagers[stageNum - 1].checkPoints[indexNum - 1].cp_name;

        // UI�̐���
        if (checkpointUI == null)
        {
            checkpointUI = Instantiate(cp_UI);
            checkpointUI.transform.Find("backGround2/Description").GetComponent<TextMeshProUGUI>().text = description;
            checkpointUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = name;
       
            // UI�ړ�����
            StartDeckPos = checkpointUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-530, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());

            // �t���O�̐؂�ւ�
            isFinished = false;
        }       
    }

    /// <summary>
    /// �`�F�b�N�|�C���gUI�̍폜
    /// </summary>
    public void DestroyUI()
    {
        if (checkpointUI != null && isFinished)
        {      
            // �t���O�̐؂�ւ�
            isDestroy = true;
            isFinished = false;

            // UI�ړ�����
            StartDeckPos = checkpointUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-1400, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());           
        }
    }

    private IEnumerator MoveUI()
    {       
        float animDuration = 0.5f; // �A�j���[�V�����̑�����
        float startTime = Time.time;
        while (Time.time - startTime < animDuration)
        {
            float journeyFraction = (Time.time - startTime) / animDuration;
            journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
            checkpointUI.transform.GetChild(0).localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
            yield return null;
        }

        isFinished = true;       
    }

}
