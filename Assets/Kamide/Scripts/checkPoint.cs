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
    }

    private void OnTriggerEnter(Collider collider)
    {
        // �^�O�ɂ���ĐG�����I�u�W�F�N�g���w��
        if (collider.tag == "Player" && isUIAppeared == false)
        {            
            // UI�\��
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().CreateUI(stage, cp_num);

            // �Q�[�W�񕜏���

            // �X�R�A���Z
            
            // �t���O�؂�ւ�
            isUIAppeared = true;                        
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // �^�O�ɂ���Ĕ���
        if (collider.tag == "Player" && isUIAppeared == true)
        {
            // Object�擾
            GameObject gameCPM =  GameObject.Find("GameCheckPointManager");

            // �t���O�؂�ւ�
            //gameCPM.GetComponent<GameCheckPointManager>().ChangeFlag(stage, cp_num, false);

            // UI�̍폜
            gameCPM.GetComponent<GameCheckPointManager>().DestroyUI();

            // ���̃I�u�W�F�N�g�̍폜
            //Destroy(this);

            // �t���O�̐؂�ւ�
            isUIAppeared = false;
        }
    }
}
