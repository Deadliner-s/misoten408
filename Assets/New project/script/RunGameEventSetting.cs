using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // �C���X�y�N�^�[�ɕ\�����邽�߂ɕK�v
public class RunGameEventData
{
    public enum RunGameEventNameEnum
    {
        None,
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
    public RunGameEventNameEnum eventName;      // �C�x���g��
    [Header("�b����������")]
    public int cnt;                         // �b����������
    [Header("Home�A��Փx������ �N�G�X�g���N���A������")]
    public int checkPoint;                  // �`�F�b�N�|�C���g��ʉ߂�����
}


[CreateAssetMenu(fileName = "RunGameEventSetting", menuName = "Scriptable Objects/RunGameEventSetting")]
public class RunGameEventSetting : ScriptableObject
{
    public List<RunGameEventData> DataList;

    // �����^�C�����݂̂̃C���X�^���X���쐬
    public RunGameEventSetting CreateRuntimeInstance()
    {
        return Instantiate(this);
    }
}
