using JetBrains.Annotations;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("�`�F�b�N�|�C���g�ԍ�")]
    public int cp_num = 0;                  // �`�F�b�N�|�C���g�̔ԍ�
    [Header("�X�e�[�W�ԍ�")]
    public int stage = 0;
    private bool isUIAppeared = false;
    private bool isEnterCheckPointArea = false;

    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        // ��]����
        Transform transform = this.transform;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 0.2f;
        transform.eulerAngles = rotation;  
        
        // �{�^������
        if (isEnterCheckPointArea && NPCDialogueManager.instance.isTalking == false &&
            (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown(KeyCode.Return)))
        {
            if (isUIAppeared == false)
            {
                // UI�\��
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().CreateUI(stage, cp_num);

                // �t���O�̐؂�ւ�
                isUIAppeared = true;
            }
            else
            {
                // UI�폜
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().DestroyUI();

                // �t���O�̐؂�ւ�
                isUIAppeared = false;
            }

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        // �^�O�ɂ���ĐG�����I�u�W�F�N�g���w��
        if (collider.tag == "Player")
        {
            // �t���O�̐؂�ւ�
            isEnterCheckPointArea = true;           
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // �^�O�ɂ���Ĕ���
        if (collider.tag == "Player")
        {
            if (isUIAppeared == true)
            {
                // UI�폜
                GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().DestroyUI();
            }

            // �t���O�̐؂�ւ�
            isUIAppeared = false;
            isEnterCheckPointArea = false;
        }
    }
}
