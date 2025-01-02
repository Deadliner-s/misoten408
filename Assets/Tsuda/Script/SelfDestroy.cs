using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // 時間(秒)を設定
    public float timeToDestroy = 5.0f;

    void Start()
    {
        // 指定した時間後にこのオブジェクトを削除
        Destroy(gameObject, timeToDestroy);
    }
}
