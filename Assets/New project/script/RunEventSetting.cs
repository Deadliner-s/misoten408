using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �C���X�y�N�^�[�ɕ\�����邽�߂ɕK�v
public class RunEventData
{
    public enum RunEventNameEnum
    {

        HomeA���,
        HomeA�������,
        HomeA�㋉���,
        HomeB���,
        HomeB�������,
        HomeB�㋉���,
        HomeC�������,
        HomeC�㋉���,
        Quest1,
        Quest2,
        Quest3,
    }

    [Header("�C�x���g��")]
    public RunEventNameEnum eventName;      // �C�x���g��
    [Header("�b����������")]
    public int cnt;                         // �b����������
    [Header("Home�A��Փx������ �N�G�X�g���N���A������")]
    public int checkPoint;                  // �`�F�b�N�|�C���g��ʉ߂�����
}


[CreateAssetMenu(fileName = "RunEventSetting", menuName = "Scriptable Objects/RunEventSetting")]
public class RunEventSetting : ScriptableObject
{
    public List<RunEventData> DataList;

    // �����^�C�����݂̂̃C���X�^���X���쐬
    public RunEventSetting CreateRuntimeInstance()
    {
        return Instantiate(this);
    }
}
