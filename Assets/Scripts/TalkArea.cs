using UnityEngine;

public class TalkArea : MonoBehaviour
{
    [Header("�C�x���g��")]
    public EventData.EventNameEnum eventName;    // �C�x���g��

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug�pF2�������Ƌ����I�Ƀ`�F�b�N�|�C���g��ʉ�
        if (Input.GetKeyDown(KeyCode.F2))
        {
            NPCDialogueManager.instance.SetCheckPoint(EventData.EventNameEnum.�p�^�[�S���t);
        }
    }
}
