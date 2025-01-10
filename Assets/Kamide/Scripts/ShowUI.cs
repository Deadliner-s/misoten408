using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [Header("�\������")]
    public float limitTime;
    [Header("UI�ړ�����")]
    public float animTime;
    public UIinfo info;
    public GameObject UI;
    GameObject createdUI;
    private Vector3 StartDeckPos;
    private Vector3 EndHandPos;
    private bool isFinished;    
    private int state;
    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �ϐ��̏�����
        isFinished = false;        
        state = 0;
        time = 0.0f;        

        // UI�̐���
        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                break;

            case 1:
                if (isFinished)
                {
                    // ���ԉ��Z
                    time += Time.deltaTime;

                    if (time > limitTime)
                    {
                        // UI�ړ�����
                        DestroyUI();

                        // �X�e�[�g�̐؂�ւ�
                        state = 2;

                        // �ϐ��̏�����
                        time = 0.0f;
                    }
                }
                break;

            case 2:
                if (isFinished)
                {
                    // UI�폜
                    Destroy(createdUI);

                    // �t���O�؂�ւ�
                    isFinished = false;                    

                    // �X�e�[�g�؂�ւ�
                    state = 0;
                }
                break;
        }
    }

    private void CreateUI()
    {
        // UI�̐���
        if (createdUI == null)
        {
            createdUI = Instantiate(UI);
            createdUI.transform.Find("backGround2/Description").GetComponent<TextMeshProUGUI>().text = info.description;
            createdUI.transform.Find("backGround2/checkPointName").GetComponent<TextMeshProUGUI>().text = info.name;

            // UI�ړ�����
            StartDeckPos = createdUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-530, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());

            // �t���O�̐؂�ւ�
            isFinished = false;

            // �X�e�[�g�̐؂�ւ�
            state = 1;
        }
    }

    private void DestroyUI()
    {
        if (createdUI != null && isFinished)
        {
            // �t���O�̐؂�ւ�
            isFinished = false;

            // UI�ړ�����
            StartDeckPos = createdUI.transform.GetChild(0).localPosition;
            EndHandPos = new Vector3(-1400, StartDeckPos.y, StartDeckPos.z);
            StartCoroutine(MoveUI());
        }
    }

    private IEnumerator MoveUI()
    {
        float animDuration = animTime;
        float startTime = Time.time;
        while(Time.time - startTime < animDuration)
        {
            float journeyFraction = (Time.time - startTime) / animDuration;
            journeyFraction = Mathf.SmoothStep(0f, 1f, journeyFraction);
            createdUI.transform.GetChild(0).localPosition = Vector3.Lerp(StartDeckPos, EndHandPos, journeyFraction);
            yield return null;
        }

        isFinished = true;
    }

    [System.Serializable]
    public  class UIinfo
    {
        [Header("���O")]
        public string name;
        [Header("������")]
        public string description;
    }

}
