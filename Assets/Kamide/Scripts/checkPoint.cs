using JetBrains.Annotations;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("�`�F�b�N�|�C���g�ԍ�")]
    public int cp_num = 0;                  // �`�F�b�N�|�C���g�̔ԍ�
    [Header("�X�e�[�W�ԍ�")]
    public int stage = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ��]����
        Transform transform = this.transform;
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 0.1f;
        transform.eulerAngles = rotation;

    }

    private void OnTriggerEnter(Collider collider)
    {
        // �^�O�ɂ���ĐG�����I�u�W�F�N�g���w��
        if (collider.tag == "Player")
        {
            // �t���O�؂�ւ�
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().ChangeFlag(stage, cp_num, false);

            // �Q�[�W�񕜏���

            // �X�R�A���Z

            // ���̃I�u�W�F�N�g�̍폜
            Destroy(this);
        }
    }
}
