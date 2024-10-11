using UnityEngine;

public class StageCheckPointManager : MonoBehaviour
{
    // �ϐ��錾
    [Header("�`�F�b�N�|�C���gPrefab")]
    public GameObject obj;       
    public CheckPoint[] checkPoints;    // �`�F�b�N�|�C���g�N���X�z��        


    void Start()
    {
        // ���̃I�u�W�F�N�g��j�󂵂Ȃ��悤�ɂ���
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            
        }
    }

    /// <summary>
    /// �������擾�֐�
    /// </summary>
    /// <param name="cp_num">�擾����`�F�b�N�|�C���g�̔ԍ�</param>
    /// <returns>�Ώۃ`�F�b�N�|�C���g�̐�����</returns>
    public string GetDescription(int cp_num)
    {
        // �͈͓�����
        if (cp_num > checkPoints.Length ||
            cp_num < 1)
            return null;

        // ��������
        cp_num--;

        // �߂�l
        return checkPoints[cp_num].cp_description;
    }

    /// <summary>
    /// �`�F�b�N�|�C���g���擾�֐�
    /// </summary>
    /// <param name="cp_num">�擾����`�F�b�N�|�C���g�̔ԍ�</param>
    /// <returns>�Ώۃ`�F�b�N�|�C���g�̖��O</returns>
    public string GetName(int cp_num)
    {
        // �͈͓�����
        if (cp_num > checkPoints.Length ||
            cp_num < 1)
            return null;

        // ��������
        cp_num--;

        // �߂�l
        return checkPoints[cp_num].cp_name;
    }
    

    /// <summary>
    /// �`�F�b�N�|�C���g��S�Đ���
    /// </summary>
    public void CreateAllCheckPoints()
    {
        // �t���O��true�̃`�F�b�N�|�C���g�𐶐�
        for (int i = 0; i < checkPoints.Length; i++)
        {
            // ���t���O��false�̏ꍇ�������X�L�b�v
            if (checkPoints[i].cp_isWorked == false ||
                checkPoints[i] == null)
            {
                continue;
            }

            // �I�u�W�F�N�g�̐���
            GameObject checkpoint = Instantiate(obj,
                new Vector3(
                checkPoints[i].cp_position.x,
                checkPoints[i].cp_position.y,
                checkPoints[i].cp_position.z),
                Quaternion.Euler(90, 0, 0));
       
            // �l�̑��
            checkpoint.GetComponent<checkPoint>().cp_num = checkPoints[i].cp_num;
        }
    }

    /// <summary>
    /// �w��̃`�F�b�N�|�C���g��1����
    /// </summary>
    /// <param name="cp_num"></param>
    public void CreateCheckPoint(int cp_num)
    {
        // ���t���O��false�̏ꍇ�������X�L�b�v
        if (checkPoints[cp_num].cp_isWorked == false ||
            checkPoints[cp_num] == null)
        {
            return;
        }

        // �I�u�W�F�N�g�̐���
        GameObject checkpoint = Instantiate(obj,
            new Vector3(
                checkPoints[cp_num].cp_position.x,
                checkPoints[cp_num].cp_position.y,
                checkPoints[cp_num].cp_position.z),
                 Quaternion.Euler(90, 0, 0));

        // �l�̑��
        checkpoint.GetComponent<checkPoint>().cp_num = cp_num;
    }

    // �`�F�b�N�|�C���g�N���X
    [System.Serializable]
    public  class CheckPoint
    {       
        public string cp_name;  
        [Header("�`�F�b�N�|�C���g�ԍ�")]
        public int cp_num;
        [Header("������")]
        public string cp_description;
        [Header("�ݒu���W")]
        public Vector3 cp_position;
        [Header("�ғ��t���O")]
        public bool cp_isWorked;
    }
}

