using System;
using UnityEngine;

public class EventData : MonoBehaviour
{
    public enum EventNameEnum
    {
        �p�^�[�S���t,
        ���X�̐l�Ƙb����,
    }

    [Header("�C�x���g��")]
    public EventNameEnum eventName;    // �C�x���g��

    [Header("���y�Y�̉摜")]
    public Texture2D icon;             // �Ⴆ�邨�y�Y�̉摜

    [NonSerialized]
    public int Cnt;                    // �b����������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
