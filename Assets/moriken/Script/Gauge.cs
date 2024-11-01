using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;   // �Q�[�W�̒l�������Ă���I�u�W�F�N�g

    private Image image;    // �ύX����摜
    private Player player;  // �Q�[�W�̒l��ێ����Ă���X�N���v�g

    private float maxParameter;     // �Q�[�W�̍ő�l
    private float parameter;        // ���݂̃Q�[�W�̒l

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = gameObject.GetComponent<Image>();   // �Q�[���I�u�W�F�N�g��Image���擾

        playerObject = GameObject.FindGameObjectWithTag("Player");  // Player�^�O�����Ă���I�u�W�F�N�g���擾
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();   // Player�^�O�����Ă���I�u�W�F�N�g����Player�X�N���v�g���擾
            image.fillAmount = player.maxBoost / 100;   // FillAmount�ɃQ�[�W�̍ő�l����
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            image.fillAmount = player.currentBoost / 100;   // ���݂̃Q�[�W�̒l���擾���AFillAmount�ɑ��
        }
    }
}
