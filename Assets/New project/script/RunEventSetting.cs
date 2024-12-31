using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // インスペクターに表示するために必要
public class RunEventData
{
    public enum RunEventNameEnum
    {

        HomeA解放,
        HomeA中級解放,
        HomeA上級解放,
        HomeB解放,
        HomeB中級解放,
        HomeB上級解放,
        HomeC中級解放,
        HomeC上級解放,
        Quest1,
        Quest2,
        Quest3,
    }

    [Header("イベント名")]
    public RunEventNameEnum eventName;      // イベント名
    [Header("話しかけた回数")]
    public int cnt;                         // 話しかけた回数
    [Header("Home、難易度を解除 クエストをクリアしたか")]
    public int checkPoint;                  // チェックポイントを通過したか
}


[CreateAssetMenu(fileName = "RunEventSetting", menuName = "Scriptable Objects/RunEventSetting")]
public class RunEventSetting : ScriptableObject
{
    public List<RunEventData> DataList;

    // ランタイム中のみのインスタンスを作成
    public RunEventSetting CreateRuntimeInstance()
    {
        return Instantiate(this);
    }
}
