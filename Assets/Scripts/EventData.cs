using System;
using UnityEngine;

public class EventData : MonoBehaviour
{
    public enum EventNameEnum
    {
        パターゴルフ,
        お店の人と話して,
    }

    [Header("イベント名")]
    public EventNameEnum eventName;    // イベント名

    [Header("お土産の画像")]
    public Texture2D icon;             // 貰えるお土産の画像

    [NonSerialized]
    public int Cnt;                    // 話しかけた回数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
