using UnityEngine;

[CreateAssetMenu(fileName = "AreaTransitionData", menuName = "Scriptable Objects/AreaTransitionData")]
public class AreaTransitionData : ScriptableObject
{
    public string areaName;
    public string targetScene;
    public Vector3 spawnPosition;
}
