using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // インスペクターに表示するために必要
public class EventData
{
    public enum EventNameEnum
    {
        パターゴルフ,
        お店の人と話して,
        キャンプ,
        おばあちゃんの昔話,
        雨宿りラーメン,
        月山神社にお参り,
        月山和紙ができるまで,
        日本一の巨樹,
        ランタンづくり体験
    }

    [Header("イベント名")]
    public EventNameEnum eventName;    // イベント名
    [Header("お土産の画像")]
    public Texture2D tex;              // 貰えるお土産の画像
    [Header("話しかけた回数")]
    public int cnt;                    // 話しかけた回数
    [Header("チェックポイントを通過したか")]
    public int checkPoint;             // チェックポイントを通過したか
}

[CreateAssetMenu(fileName = "EventSetting", menuName = "Scriptable Objects/EventSetting")]
public class EventSetting : ScriptableObject
{
    public List<EventData> DataList;

    // ランタイム中のみのインスタンスを作成
    public EventSetting CreateRuntimeInstance()
    {
        return Instantiate(this);
    }
}
