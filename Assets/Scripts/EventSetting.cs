using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �C���X�y�N�^�[�ɕ\�����邽�߂ɕK�v
public class EventData
{
    public enum EventNameEnum
    {
        �p�^�[�S���t,
        ���X�̐l�Ƙb����,
        �L�����v,
        ���΂������̘̐b,
        �J�h�胉�[����,
        ���R�_�Ђɂ��Q��,
        ���R�a�����ł���܂�,
        ���{��̋���,
        �����^���Â���̌�
    }

    [Header("�C�x���g��")]
    public EventNameEnum eventName;    // �C�x���g��
    [Header("���y�Y�̉摜")]
    public Texture2D tex;              // �Ⴆ�邨�y�Y�̉摜
    [Header("�b����������")]
    public int cnt;                    // �b����������
    [Header("�`�F�b�N�|�C���g��ʉ߂�����")]
    public int checkPoint;             // �`�F�b�N�|�C���g��ʉ߂�����
}

[CreateAssetMenu(fileName = "EventSetting", menuName = "Scriptable Objects/EventSetting")]
public class EventSetting : ScriptableObject
{
    public List<EventData> DataList;

    // �����^�C�����݂̂̃C���X�^���X���쐬
    public EventSetting CreateRuntimeInstance()
    {
        return Instantiate(this);
    }
}
