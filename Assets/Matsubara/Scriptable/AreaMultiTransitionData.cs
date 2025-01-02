using UnityEngine;

[CreateAssetMenu(fileName = "AreaMultiTransitionData", menuName = "Game/AreaMultiTransitionData")]
public class AreaMultiTransitionData : ScriptableObject
{
    [System.Serializable]
    public class TransitionInfo
    {
        public string SceneName;      // 遷移方向や条件を示す（例: "North", "South", etc.）
        public string targetScene;    // 遷移先のシーン名
        public Vector3 spawnPosition; // 遷移先のスポーン位置
        public GameObject transform;
    }

    public string areaName;  // エリア名
    public TransitionInfo[] transitions;  // 複数の遷移先情報
}
