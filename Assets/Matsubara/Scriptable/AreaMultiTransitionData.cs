using UnityEngine;

[CreateAssetMenu(fileName = "AreaMultiTransitionData", menuName = "Game/AreaMultiTransitionData")]
public class AreaMultiTransitionData : ScriptableObject
{
    [System.Serializable]
    public class TransitionInfo
    {
        public string SceneName;      // �J�ڕ���������������i��: "North", "South", etc.�j
        public string targetScene;    // �J�ڐ�̃V�[����
        public Vector3 spawnPosition; // �J�ڐ�̃X�|�[���ʒu
        public GameObject transform;
    }

    public string areaName;  // �G���A��
    public TransitionInfo[] transitions;  // �����̑J�ڐ���
}
